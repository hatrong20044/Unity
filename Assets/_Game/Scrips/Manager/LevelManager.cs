using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.Barracuda.BurstCPUOps;

public class LevelManager : Singleton<LevelManager>//quan li cac cap do,hinh anh,nhan vat tro choi nhu kieu khi bat dau no se hien cai gi len,va thucj hien cai gi ne 
{
    readonly List<ColorType> colorTypes = new List<ColorType>() {    ColorType.Blue,   ColorType.Yellow ,  ColorType.Green,   ColorType.Violet,   ColorType.Orange};//gan gia tri mau cu the cho ds colorTypes
    //readonly dc sd de khai bao 1 bien ma chi co the dc gan gia tri 1 lan va gia tri do k the thay doi 
    public Level[] levelPrepabs;
    public Bot botPrefab;
    public Player player;
    private Level currentLevel;
    private Stage stage;
    private int levelIndex;
    
    private void Awake()
    {
       
        levelIndex = PlayerPrefs.GetInt("Level", 0);
        //lay gia tri levelIndex tu PlayerPrefs khi doi tuong dc khoi tao ,giup luu tru va khoi phuc trang thai cap do tro choi 
        //neu k co gtri nao luu tru voi key la "Level" gia tri mac dinh la 0 
    }
    public int CharacterAmount => currentLevel.botAmount +1 ;//tinh toan so nhan vat dua tren so luong bot trong level htai + them 1(chac them player)

    public Vector3 FinishPoint=>currentLevel.finishPoint.position;//diem hoan thanh tro choi =vi tri hien tai cua finishpoint trong level htai 
    private List<Bot>bots=new List<Bot>();
    private void Start()
    {
         LoadLevel(levelIndex);
         Oninit();
         UIManager.Instance.OpenUI<MainMenu>();
    }
    public void Oninit()
    {
        //ClearAllBricksFromOtherClass();
        //init vitri bat dau game
        Vector3 index = currentLevel.startPoint.position;//lay vi tri bat dau tro choi o level htai gan chi index
        float space=2f;//khoang cach giua cac nv
        
        Vector3 leftPoint=((CharacterAmount/2)+(CharacterAmount % 2)*0.5f-0.5f)*space*Vector3.left+index;//tinh toan vi tri giua cac nv dua tren so nv 
        List<Vector3>startPoint=new List<Vector3>();//khoi tao 1 ds cac diem bdau

        for(int i = 0; i < CharacterAmount; i++)//i<so nv htai
        {
            startPoint.Add(leftPoint+space*Vector3.right*i);//them vi tri vao ds startPoint,tao 1 loat cac vi tri ,moi vtri cac nhau space theo huong phai,noi chung quan li vi tri nvat) 
            
        }
        //init random mau 
        List<ColorType> colorDatas = Utilities.SortOrder(colorTypes, CharacterAmount);//khoi tao ds colordata

        //set vi tri cho player
        int rand = Random.Range(0, CharacterAmount);
        Vector3 playerStartPosition = startPoint[rand];
        playerStartPosition.y -= 0.4f; // Giảm vị trí y đi 1 đơn vị4
        player.transform.position = playerStartPosition;
        player.transform.rotation = Quaternion.identity;
        startPoint.RemoveAt(rand);

        //set color Player
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);//RemoveAt() được sử dụng để xóa phần tử khỏi một danh sách theo chỉ mục cụ thể.
        //co nghĩa là màu mình vừa dùng sẽ bị xóa khỏi danh sách (mấy con bot khác dẽ k đụng màu)

        player.Oninit();//set lai góc quay và xóa gachj hiện có

        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            Bot bot = Instantiate(botPrefab, startPoint[i], Quaternion.identity);
            //Bot bot = SimplePool.Spawn<Bot>(botPrefab, startPoint[i], Quaternion.identity);
            bot.ChangeColor(colorDatas[i]);//set mau cho bot
            bot.Oninit();//khoi tao tt bot
            bots.Add(bot);//them bot 

           
        }

    }
    //public void ClearAllBricksFromOtherClass()
    //{
    //    stage.ClearAllBricks();
    //}
    public void LoadLevel(int level)//khoi tao level
    {
        Debug.Log("1 " + level);

        if (currentLevel != null)
        {//neu no dang co 1 level nao do thi no se bi xoa 
            Destroy(currentLevel.gameObject);
        }
        if(level<levelPrepabs.Length)//neu k co level nao 
        {
            Debug.Log("122 " + level);
            currentLevel = Instantiate(levelPrepabs[level]);//tao 1 level moi voi tham so level 
            
            currentLevel.OnInit();//khoi tao cap do level moi 
        }
        else
        {
            //TODO:
        }
    }
    public void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);//thay doi tt tro choi sang tt GamePlay
        for(int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new PatrolState());//tao 1 trang thai moi cho bot (tuc la ve vi tri ban dau)

        }
    }
    public void OnFinishGame()
    {
        for(int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(null);//dat trang thai cua bot ve mac dinh , dung hanh vi
            bots[i].MoveStop();//dung di chuyen cua bot
        }
    }
    public void OnReset()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            Destroy(bots[i].gameObject);//xoa bot tren scene
        }
        bots.Clear();//xoa het cac bot trong danh sach
        
    }

    internal void OnRetry()//làm lại
    {
        OnReset();
        
        LoadLevel(levelIndex);
        Oninit();
        UIManager.Instance.OpenUI<MainMenu>();
    }

    internal void OnNextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
        LoadLevel(levelIndex);// nghĩ phải là LoadLevel(levelIndex++);
        Oninit();
        UIManager.Instance.OpenUI<MainMenu>();
    }
}
