using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed;
   // [SerializeField] private Rigidbody rb;
  
    
    
    
    
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) )
        {
            if (Input.GetMouseButton(0)) {
                Vector3 nextPoint = JoystickControl.direct * speed * Time.deltaTime + transform.position;
                if (CanMove(nextPoint))
                {
                    transform.position = CheckGround(nextPoint);
                }


                if (JoystickControl.direct != Vector3.zero)
                {
                    skin.forward = JoystickControl.direct;
                }
                ChangeAnim("run");
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            ChangeAnim("idle");
        }

    }
    //check diem tiep theo co phai la ground khong
    //+ tra ve vi tri next do
    //- tra ve vi tri hien tai
    
}
