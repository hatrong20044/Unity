using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform startPoint;
    public Transform finishPoint;
    public int botAmount;//so luong bot
    public Stage[] stages;//luu tru nhieu so luong stage ,dai dien cho tung giai doan khac nhau cua cap do
    public void OnInit()//khoi tao cap do 
    {

        for(int i = 0; i < stages.Length; i++)
        {
            
            stages[i].OnInit();
        }
    }
  
}
