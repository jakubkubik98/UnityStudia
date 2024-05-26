using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     public CharacterController characterController;

    public float movementSpeed = 10.0f;
    public float runningSpeed = 5.0f;

    public float vertical = 0f;
    public float horizontal = 0f;

    public float currentHeight = 0f;
    public float jumpHeight = 10f;

    public float sensitivity = 3.0f;
    public float verticalRange = 90.0f;
    public float mouseVertical = 0.0f;
    public float mouseHorizontal = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
         characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardController();
        MouseController();
    }

    void KeyboardController()
    {
        vertical = Input.GetAxis("Vertical")* movementSpeed;
        horizontal = Input.GetAxis("Horizontal") * movementSpeed;

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            currentHeight = jumpHeight;
        } else if (!characterController.isGrounded) { currentHeight += Physics.gravity.y * Time.deltaTime; }
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        { 
            movementSpeed += runningSpeed; 
        } 
        else if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            movementSpeed -= runningSpeed;
        };
        Vector3 move = new Vector3(horizontal, currentHeight, vertical);
        move = transform.rotation * move;
        characterController.Move(move * Time.deltaTime);
    }

    void MouseController()
    {
        mouseHorizontal = Input.GetAxis("Mouse X") * sensitivity;
        mouseVertical -= Input.GetAxis("Mouse Y") * sensitivity;

        transform.Rotate(0, mouseHorizontal, 0);

        mouseVertical = Mathf.Clamp(mouseVertical, -verticalRange, verticalRange);
        Camera.main.transform.localRotation = Quaternion.Euler(mouseVertical, 0, 0);
    }
}
