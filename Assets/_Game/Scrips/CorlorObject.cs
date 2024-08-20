using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorlorObject : GameUnit
{
    public ColorType colorType;
    
    [SerializeField] private Renderer renderer;
    [SerializeField] private ColorData colorData;
    // Start is called before the first frame update
    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = colorData.GetColorMat(colorType);
    }
    
    
}
