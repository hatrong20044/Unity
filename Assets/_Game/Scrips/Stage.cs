using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ColorType
{
    Default,
    Blue,
    Yellow,
    Green,
    Violet,
    Orange,
}
public class Stage : MonoBehaviour
{
    public Transform[] brickPoint;
    public List<Vector3> emptyPoint= new List<Vector3>();//vi tri
    public List<Brick> bricks= new List<Brick>();
    [SerializeField] Brick brickPrefab;
   


    internal void OnInit()
    {
        
        for (int i = 0; i < brickPoint.Length; i++)
        {
            emptyPoint.Add(brickPoint[i].position);//tao ra 1 ban sao cac vi tri gach tu brickPoint
        }
    }

    //public void ClearAllBricks()
    //{
    //    // Destroy all Brick objects
    //    foreach (Brick brick in bricks)
    //    {
    //        SimplePool.Despawn(brick);
    //    }

    //    // Clear the lists
    //    bricks.Clear();
    //    emptyPoint.Clear();
    //}
    public void InitColor(ColorType colorType)//reset mau 
    {
        
        int amount = brickPoint.Length / LevelManager.Instance.CharacterAmount;//chia deu so gach va so mau 
        for(int i=0; i <amount; i++)
        {
            NewBrick(colorType);
        }
    }
    
    public void NewBrick(ColorType colorType)
    {
        if (emptyPoint.Count > 0)
        {
            int rand=Random.Range(0, emptyPoint.Count);//ngau nhien vi tri cac vien gach
            Brick brick= SimplePool.Spawn<Brick>(brickPrefab, emptyPoint[rand],Quaternion.identity);
            brick.stage = this;
            brick.ChangeColor(colorType);
            emptyPoint.RemoveAt(rand);
            bricks.Add(brick);
        }
    }
    internal void RemoveBrick(Brick brick)//loai bo vien gach khoi ds brick
    {
        emptyPoint.Add(brick.transform.position);//them vi tri gach vao ds emptypoint(co nghia la sau khi gach dc loai bo vi tri do se trong va co the sd cho cac gacgh khac)
        bricks.Remove(brick);//loai bo gach khoi s

    }

    internal Brick SeekBrickPoint(ColorType colorType)//tim kiem va tra ve cac vien gach co mau sac chi dinh tu ds bricks
    {
       Brick brick= null;
        for(int i=0; i<bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                brick= bricks[i];
                break;
            }
            
        }
        return brick;
    }

    
}
