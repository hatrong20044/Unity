using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : CorlorObject
{
    [HideInInspector]public Stage stage;
    public void OnDespawn()
    {
        stage.RemoveBrick(this); //this la chinh no
    }
    
}
