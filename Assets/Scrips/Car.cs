using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float tocDoXe = 100f;
    private float dauVaoDichuyen;
    private Rigidbody rb;
    [SerializeField] private float lucReXe = 100f;
    private float dauVaoRe;
    [SerializeField] private float lucPhanh = 50f;
    [SerializeField] private GameObject hieuUngPhanh;
    private Vector2 mousePreviousPos, mouseCurrentPos;
    private bool isMousePressed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Xử lý sự kiện chuột
        if (Input.GetMouseButtonDown(0))
        {
            mousePreviousPos = Input.mousePosition;
            isMousePressed = true;
        }
        else if (Input.GetMouseButton(0))
        {
            mouseCurrentPos = Input.mousePosition;
            dauVaoDichuyen = (mouseCurrentPos.y - mousePreviousPos.y) / Screen.height * 2;
            dauVaoRe = (mouseCurrentPos.x - mousePreviousPos.x) / Screen.width * 2;
            DiChuyenXe();
            ReXe();
            mousePreviousPos = mouseCurrentPos;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }

        // Xử lý phanh
        if (dauVaoDichuyen > 0 && isMousePressed)
        {
            PhanhXe();
        }
    }

    public void DiChuyenXe()
    {
        rb.AddRelativeForce(-Vector3.forward * dauVaoDichuyen * tocDoXe);
        hieuUngPhanh.SetActive(false);
    }

    public void ReXe()
    {
        Quaternion re = Quaternion.Euler(Vector3.up * dauVaoRe * lucReXe * Time.deltaTime);
        rb.MoveRotation(rb.rotation * re);
    }

    public void PhanhXe()
    {
        if (rb.velocity.z != 0)
        {
            rb.AddRelativeForce(-Vector3.forward * lucPhanh);
            hieuUngPhanh.SetActive(true);
        }
    }
}