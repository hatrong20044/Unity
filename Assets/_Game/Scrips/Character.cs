using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : CorlorObject
{
    [SerializeField] private LayerMask stairLayer;
    private List<PlayerBrick> playerBricks = new List<PlayerBrick>();
    [SerializeField] private PlayerBrick PlayerBrickPrefab;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] protected Transform skin;
    public Animator anim;
    private string currentAnim;
    [SerializeField] private Transform brickHolder;
    [HideInInspector]public Stage stage;

    public int BrickCount => playerBricks.Count;//tra ve so luong vien gach co trong danh sach playerbricks

    public virtual void Oninit()
    {
        ClearBrick();
        skin.rotation= Quaternion.identity; //goc quay cua skin ve gia tri mac dinh (Quaternion.indenty)
    }
    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;//luu tru thong tin ve va cham 
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {//phat 1 tia ray tu vi tri nextpoint,huong xuong duoi,
            //out hit luu tru thong tin ve va cham neu co,2f la khoang cach tia ray,groundLayer xác định các lớp mà tia sẽ tuong tác với
            //nếu nếu tua va chạm với lớp layer,bên trong câu lệnh if dc thực hiện,
            return hit.point + Vector3.up * 1.1f;//trả về điểm va chạm hit.point một độ lệch Vector3.up nhân với 1.1f.
                                                 ////Điều này được thực hiện để đảm bảo rằng vị trí trả về nằm phía trên mặt đất một chút.
        }
        return transform.position;
    }
    public bool CanMove(Vector3 nextPoint) //canmove=len cau
    {
        //tham số nextPoint kiểu Vector3 và trả về một giá trị kiểu bool để xác định xem người chơi có thể di chuyển tới nextPoint hay không.
        bool isCanmove = true;
        RaycastHit hit;//luu tru thong tin ve va cham 
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stairLayer))
        {
            Stair stair = hit.collider.GetComponent<Stair>();// lấy thành phần  Stair từ đối tượng va chạm được lưu trữ trong biến hit.
            if (stair.colorType != colorType && playerBricks.Count > 0)


            {
                stair.ChangeColor(colorType);
                RemoveBrick();
                stage.NewBrick(colorType);
            }
            if (stair.colorType != colorType && playerBricks.Count == 0 && skin.forward.z > 0)
            {
                isCanmove = false;
            }
        }
        return isCanmove;
    }
    public void AddBrick()
    {
        PlayerBrick playerBrick = Instantiate(PlayerBrickPrefab, brickHolder);
        playerBrick.ChangeColor(colorType);
        playerBrick.transform.localPosition = Vector3.up * 0.5f * playerBricks.Count;
        playerBricks.Add(playerBrick);//them playerBrick vao danh sach playerBricks de quan li 

    }
    public void RemoveBrick()
    {
        if (playerBricks.Count > 0)//neu so gach co trong danh danh lon hon 0
        {
            PlayerBrick playerBrick = playerBricks[playerBricks.Count - 1];//lay phan tu cuoi cung trong danh sach playerbricks va gan cho bien playerBrick
            playerBricks.RemoveAt(playerBricks.Count - 1);//xoa phan tu cuoi cung trong danh sach playerBricks
            Destroy(playerBrick.gameObject);//huy doi tuong game cua vien gach ,(xoa khoi scene va giai phong bo nho  

        }
    }
    public void ClearBrick()
    {
        for (int i = 0; i < playerBricks.Count; i++)
        {
            
            Destroy(playerBricks[i].gameObject);//xoa vien gach thu i trong scene 
            

        }
        playerBricks.Clear();//xoa sach danh sach playerBricks 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            
            Brick brick = other.GetComponent<Brick>();
            if (brick.colorType == colorType)//mau cua doi tuong gach va cham == mau cua nguoi choi 
            {
                brick.OnDespawn();//xoa vien gach do khoi stage 
                AddBrick();//them gach vao vi tri brickholder
                Destroy(brick.gameObject);//huy vien gach trong scene 

            }
        }
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
    // Start is called before the first frame update
    

