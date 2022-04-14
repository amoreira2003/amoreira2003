using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;

    float horizontalMovement;
    float verticalMovement;


    [Header("Movement")]
    public float gravityMultiplier = 2f;
    public float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    public float speedMultiplier = 10f;
    public float jumpForce = 5f;

    [Header("Crouch")]
    bool isCrouching = false; 

    [Header("Sprinting")]

    [SerializeField] float crouchSpeed = 2f;
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;


    [Header("Ground Settings")]
    public float groundDistance = 0.2f;
    [SerializeField] LayerMask groundMask;
    public bool isGrounded;
    [SerializeField] Transform groundCheck;


    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Drag Settings")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float onAirDrag = 2f;

    [Header("Slop Settings")]
    RaycastHit slopHit;
    Vector3 slopeMoveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopHit, 1.5f))
        {
            if (slopHit.normal != Vector3.up) return true;
        }
        return false;
    }


    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {   
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            isCrouching = false;
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            isCrouching = false;

        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        getInput();
        setDrag();
        ControlSpeed();
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopHit.normal);
    }

    private void FixedUpdate()
    {
        movePlayer();
    }
    void getInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

    }


    void getCrouch() {
        Input.GetKey(crouchKey);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void setDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = onAirDrag;
        }
    }
    void movePlayer()
    {
        if (!isGrounded && rb.useGravity)
        {
            rb.AddForce(Vector3.down * -gravityMultiplier, ForceMode.Acceleration);
        }

        if (isGrounded && !onSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * speedMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && onSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * speedMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * speedMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}
