using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public static Vector3 direct;
    private Vector3 screen;
    private Vector3 startpoint;
    private Vector3 updatePoint;
    private Vector3 MousePosition => Input.mousePosition - screen / 2;
    public RectTransform joystickBG;
    public RectTransform joystickControl;
    public float magnitude;
    public GameObject joystickPanel;
    // Start is called before the first frame update
    void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;

        direct = Vector3.zero;
        joystickPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startpoint = MousePosition;
            joystickBG.anchoredPosition = startpoint;
            joystickPanel.SetActive(true);
        }
        if (Input.GetMouseButton(0))
        {
            updatePoint = MousePosition;
            joystickControl.anchoredPosition = Vector3.ClampMagnitude((updatePoint - startpoint), magnitude) + startpoint;
            direct = (updatePoint - startpoint).normalized;
            direct.z = direct.y;
            direct.y = 0;
        }
        if (Input.GetMouseButtonUp(0))
        {
            joystickPanel.SetActive(false);
            direct = Vector3.zero;
        }
    }
    private void OnDisable()
    {
        direct = Vector3.zero;
    }
}
