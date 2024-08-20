using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : UICanvas
{
   public void NextButton()
    {
        LevelManager.Instance.OnNextLevel();
        Close(0.1f);
    }
    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        Close(0.1f);
    }
}
