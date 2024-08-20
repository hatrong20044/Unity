using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Vector3 destionation;
    public bool IsDestination => Vector3.Distance(destionation, Vector3.right*transform.position.x + Vector3.forward*transform.position.z) < 0.1f;
    //Destination:đích 

    //protected override void Start()
    //{
    //    base.Start();
    //    ChangeState(new PatrolState());
    //}
    public override void Oninit()
    {
        base.Oninit();//tu khoa base để phân biệt giữa lớp cha và con nếu cái hàm nó giống nhau (kia là nó gọi lớp cha ) 
        ChangeAnim("idle");
    }
    public void SetDestination(Vector3 position)//trang thai di tim
        //them 1 tham so vi tri 
    {
        
        agent.enabled = true;//kich hoat navmesh
        destionation = position;
        destionation.y = 0;
        agent.SetDestination(position);
        
    }
    IsState<Bot> currentState;
    private void Update()
    {
      if(GameManager.Instance.IsState(GameState.Gameplay)&& currentState != null)
        {//check stair
            
            currentState.OnExecute(this);// đang duy tri trang thai currentState 
            //check stair
            CanMove(transform.position);//kiem tra kha nang di chuyen cua no 
        }        
    }
    public void ChangeState(IsState<Bot> state)//thay doi trang thai (truyen vao 1 tham so state ,truyen ts dc sd khi ban muon thay doi dieu gì đó)
    {
        if(currentState != null)//trang thai hien tai khac nulll
        {
            currentState.OnExit(this); //thực hiện các hành động cần thiết khi rời khỏi trạng thái hiện tại.
        }
        currentState = state;//trang thai htai = trang thai moi
        if(currentState != null )
        {
            currentState.OnEnter(this);//để thực hiện các hành động khi bat dau vào trạng thái mới.
        }
    }

    internal void MoveStop()//internal no chi la dat  giup bao ve tinh rieng tu thoi chu no cung nhuw public nhung chi co the truy cap tu project cua b
    {
        agent.enabled = false;//ngung di chuyen (TAT NAVMESH)
    }
}
