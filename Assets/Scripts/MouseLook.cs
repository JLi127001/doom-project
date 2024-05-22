using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1.0f;
    public float smoothing = .5f;
    public GameObject Camera;

    private float xMousePos;
    private float yMousePos;
    private float smoothedXMousePos;
    private float smoothedYMousePos;

    private float currentXLookPos;
    private float currentYLookPos;

    private void Start()
    {
        //lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        getInput();
        modifyInput();
        movePlayer();
        
    }
    void getInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
        yMousePos = Input.GetAxisRaw("Mouse Y");

    }

    void modifyInput()
    {
        xMousePos *= sensitivity * smoothing;
        yMousePos *= sensitivity * smoothing;
        smoothedXMousePos = Mathf.Lerp(smoothedXMousePos,xMousePos,(1f/smoothing));
        smoothedYMousePos = Mathf.Lerp(smoothedYMousePos, yMousePos, (1f / smoothing));
    }

    void movePlayer()
    {
        currentXLookPos += smoothedXMousePos;
        currentYLookPos -= smoothedYMousePos;
        currentYLookPos = Mathf.Clamp(currentYLookPos, -90f, 90f);
        transform.localRotation = Quaternion.AngleAxis(currentXLookPos,transform.up);
        Camera.transform.localEulerAngles = Vector3.right * currentYLookPos;
    }
}
