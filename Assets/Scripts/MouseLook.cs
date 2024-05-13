using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1.0f;
    public float smoothing = .5f;

    private float xMousePos;
    private float smoothedMousePos;

    private float currentLookPos;

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

    }

    void modifyInput()
    {
        xMousePos *= sensitivity * smoothing;
        smoothedMousePos = Mathf.Lerp(smoothedMousePos,xMousePos,(1f/smoothing));
    }

    void movePlayer()
    {
        currentLookPos += smoothedMousePos;
        transform.localRotation = Quaternion.AngleAxis(currentLookPos,transform.up);
    }
}
