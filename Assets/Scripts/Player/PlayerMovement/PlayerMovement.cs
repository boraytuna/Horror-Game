using UnityEngine;

// This script manages first person player movement
public class PlayerMovement : MonoBehaviour
{   
    [Header("Movement")]
    public float runSpeed; 
    public float walkSpeed;
    public float groundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouch")]
    public float crouchSpeed;
    public float crouchHeight = 0.5f;
    public bool isCrouching = false;
    private float standingHeight;
    private float standingCameraHeight;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina;
    public float staminaDepletionRate; // Stamina depleted per second while running
    public float staminaRegenerationRate; // Stamina regenerated per second when not running

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode walkKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisGround;
    bool grounded;

    [Header("Camera")]
    public GameObject cameraHolder;
    public Transform orientation;

    private float activeMoveSpeed;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        activeMoveSpeed = walkSpeed; 
        standingHeight = playerHeight; 
        standingCameraHeight = cameraHolder.transform.localPosition.y;
        stamina = maxStamina;
    }

    private void Update() 
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);
        
        MyInput(); 

        // handle drag
        if(grounded)
        {   
            rb.drag = groundDrag;
        }else 
        {
            rb.drag = 0;
        } 

        // call the functions
        CheckCrouch(); 
        Walk();
        RegenerateStamina();
    } 

    private void FixedUpdate() 
    {
        MovePlayer();
        SpeedControl();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            // Jump continuously by holding jump key
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Walk()
    {
        if(isCrouching)
        {
            activeMoveSpeed = crouchSpeed;
        }
        else
        {
            // Check if the player is trying to run and if they have enough stamina
            if(Input.GetKey(walkKey) && stamina > 0)
            {
                activeMoveSpeed = runSpeed;
                // Deplete stamina
                stamina -= staminaDepletionRate * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0, maxStamina); // Ensure stamina stays within bounds
            }
            else
            {
                // Use walk speed if not running or out of stamina
                activeMoveSpeed = walkSpeed;
            }
        }
    }

    private void CheckCrouch()
    {
        if(Input.GetKeyDown(crouchKey))
        {
            StartCrouch();
        }else if (Input.GetKeyUp(crouchKey))
        {
            EndCrouch();
        }  
    }

    private void StartCrouch()
    {
        isCrouching = true;
        // Adjust the player's collider
        GetComponentInChildren<CapsuleCollider>().height = crouchHeight;
    
        // Lower the camera
        cameraHolder.transform.localPosition = new Vector3(0, crouchHeight, 0);
    }

    private void EndCrouch()
    {
        isCrouching = false;
        // Reset the player's collider
        GetComponentInChildren<CapsuleCollider>().height = standingHeight;
        
        // Reset the camera position
        cameraHolder.transform.localPosition = new Vector3(0, standingCameraHeight, 0);
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit player's velocity
        if(flatVel.magnitude > runSpeed)
        {
            Vector3 limitedVel = flatVel. normalized * runSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

        private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * activeMoveSpeed * 10f, ForceMode.Force);
        }else if(!grounded) // on air 
        {
            rb.AddForce(moveDirection.normalized * activeMoveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
    }

    private void RegenerateStamina()
    {
        // Only regenerate stamina if not running or stamina is not full
        if(!Input.GetKey(walkKey) && stamina < maxStamina)
        {
            stamina += staminaRegenerationRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina); // Ensure stamina stays within bounds
        }
    }

}