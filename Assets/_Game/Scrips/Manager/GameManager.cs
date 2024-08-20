using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    MainMenu,Gameplay,Pause
}
public class GameManager : Singleton<GameManager>//quan li trang thai nhan vat choi ,thay doi trang thai 
{
   private GameState gameState;
    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }
      
    public void ChangeState(GameState gameState)//pt thay doi trang thai nguoi choi 
    {
        this.gameState = gameState;//cap nhat trang thai hien tai thanh trang thai moi  
    }
    public bool IsState(GameState gameState)//kiem tra xem trang thai hien tai co trung voi trang thai dc truyen vao hay k 
                                            //trả về true nếu trạng thái hiện tại trùng khớp và false nếu không
    {
        return this.gameState == gameState;
    }
}
