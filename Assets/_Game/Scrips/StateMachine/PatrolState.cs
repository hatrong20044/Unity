using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IsState<Bot>//dieu khien
{
    int targetBrick;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim("run");
        targetBrick = Random.Range(2, 7);
        SeekTarget(t);
    }

    public void OnExecute(Bot t)
    {
        
        if (t.IsDestination)//neu bot den dich
        {
            if (t.BrickCount >= targetBrick)//neu so gach ma bot an dc lon hon muc tieu gach can thu
            {
                t.ChangeState(new AttackState());//chuyen trang thai xay cau
            }
            else
            {
              SeekTarget(t);//con neu so gach thu dc it hon mtieu no se di tim gach
            }
        }
        else
        {
            SeekTarget(t);
        }
    }

    public void OnExit(Bot t)
    {

    }
    private void SeekTarget(Bot t)
    {
        
        if (t.stage != null)//neu bot dang trên san choi 
        {
            Brick brick = t.stage.SeekBrickPoint(t.colorType);//vien gach tren san maf cung mau voi bot tim dc
            if (brick == null)//neu khong tim dc nua
            {
                
                t.ChangeState(new AttackState());//bot xay cau
            }
            else
            {
                
                t.SetDestination(brick.transform.position);//neu co gach tren san,bot se di tim vi tri vien gach cung mau do
            }
        }
        else//neu bot k tren san
        {
            
            t.SetDestination(t.transform.position);//vi tri cua bot chinh la vi tri htai cua no,tuc no dung im
        }
        
    }
}
