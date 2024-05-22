using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 10f;
    private CharacterController charControl;
    public Animator gunAnim;
    public Animator camAnim;
    public float momentumDamping = 5f;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private float gravity = -9.81f;
    private bool isWalking;

    void Start()
    {
        charControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        movePlayer();

        camAnim.SetBool("isWalking", isWalking);
        gunAnim.SetBool("isWalking", isWalking);
    }

    void getInput()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 0)
        {
            isWalking = true;
            inputVector = input;
            inputVector.Normalize();
            // move in relation to camera, not world space
            inputVector = transform.TransformDirection(inputVector);
        }
        else
        {
            isWalking = false;
            // a little bit of slide at the end of the movement
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);
        }

        movementVector = (inputVector * playerSpeed) + (Vector3.up * gravity);
    }

    void movePlayer()
    {
        charControl.Move(movementVector*Time.deltaTime);

    }
}
