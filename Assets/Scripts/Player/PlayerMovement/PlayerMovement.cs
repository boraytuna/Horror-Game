using UnityEngine;
using System.Collections;

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
    private bool readyToJump;

    [Header("Crouch")]
    public float crouchSpeed;
    public float crouchHeight = 0.5f;
    public bool isCrouching = false;
    private float standingHeight;
    private float standingCameraHeight;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina;
    public float staminaDepletionRate;
    public float staminaRegenerationRate;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode walkKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisGround;
    private bool grounded;

    [Header("Camera")]
    public GameObject cameraHolder;

    private float activeMoveSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;

    private bool isRunningSoundPlaying = false;
    private bool isWalkingSoundPlaying = false;


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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);

        MyInput();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

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

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    // private void Walk()
    // {
    //     activeMoveSpeed = isCrouching ? crouchSpeed : (Input.GetKey(walkKey) && stamina > 0 ? runSpeed : walkSpeed);
    //     if (Input.GetKey(walkKey) && stamina > 0 && !isCrouching)
    //     {
    //         stamina -= staminaDepletionRate * Time.deltaTime;
    //         stamina = Mathf.Clamp(stamina, 0, maxStamina);
    //     }
    // }

    private void Walk()
    {
        float speed = rb.velocity.magnitude;
        bool isMoving = speed > 0.1f; 

        activeMoveSpeed = isCrouching ? crouchSpeed : (Input.GetKey(walkKey) && stamina > 0 ? runSpeed : walkSpeed);

        if(isMoving)
        {
            if (activeMoveSpeed == runSpeed)
            {
                if (!isRunningSoundPlaying)
                {
                    FindAnyObjectByType<AudioManager>().Stop("PlayerWalking"); // Stop walking sound if it's playing
                    FindAnyObjectByType<AudioManager>().Play("PlayerRunning");
                    isRunningSoundPlaying = true;
                    isWalkingSoundPlaying = false;
                }
            }
            else if (activeMoveSpeed == walkSpeed || activeMoveSpeed == crouchSpeed)
            {
                if (!isWalkingSoundPlaying)
                {
                    FindAnyObjectByType<AudioManager>().Stop("PlayerRunning");  // Stop running sound if it's playing
                    FindAnyObjectByType<AudioManager>().Play("PlayerWalking");
                    isWalkingSoundPlaying = true;
                    isRunningSoundPlaying = false;
                }
            }
            else
            {
                StopMovementSounds();
            }
        }
        else
        {
            StopMovementSounds();
        }
        

        if (Input.GetKey(walkKey) && stamina > 0 && !isCrouching)
        {
            stamina -= staminaDepletionRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
    }

    private void StopMovementSounds()
    {
        if (isRunningSoundPlaying)
        {
            FindAnyObjectByType<AudioManager>().Stop("PlayerRunning"); 
            isRunningSoundPlaying = false;
        }
        if (isWalkingSoundPlaying)
        {
            FindAnyObjectByType<AudioManager>().Stop("PlayerWalking");
            isWalkingSoundPlaying = false;
        }
    }

    private void CheckCrouch()
    {
        if (Input.GetKeyDown(crouchKey))
            StartCrouch();
        else if (Input.GetKeyUp(crouchKey))
            EndCrouch();
    }

    private void StartCrouch()
    {
        isCrouching = true;
        GetComponentInChildren<CapsuleCollider>().height = crouchHeight;
        StartCoroutine(AdjustCameraHeight(crouchHeight));
    }

    private void EndCrouch()
    {
        isCrouching = false;
        GetComponentInChildren<CapsuleCollider>().height = standingHeight;
        StartCoroutine(AdjustCameraHeight(standingCameraHeight));
    }

    private IEnumerator AdjustCameraHeight(float targetHeight)
    {
        float currentHeight = cameraHolder.transform.localPosition.y;
        float timeToCrouch = 0.25f;
        float elapsed = 0f;

        while (elapsed < timeToCrouch)
        {
            cameraHolder.transform.localPosition = new Vector3(0, Mathf.Lerp(currentHeight, targetHeight, elapsed / timeToCrouch), 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraHolder.transform.localPosition = new Vector3(0, targetHeight, 0);
    }

    private void Jump()
    {
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
        if (flatVel.magnitude > runSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * runSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void MovePlayer()
    {
        Vector3 forward = cameraHolder.transform.forward;
        Vector3 right = cameraHolder.transform.right;
        forward.y = 0; // Ensure the movement is only horizontal
        right.y = 0;
        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;
        float multiplier = grounded ? 1.0f : airMultiplier;
        rb.AddForce(moveDirection.normalized * activeMoveSpeed * 10f * multiplier, ForceMode.Force);
    }

    private void RegenerateStamina()
    {
        if (!Input.GetKey(walkKey) && stamina < maxStamina)
        {
            stamina += staminaRegenerationRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
    }

    public void BoostStamina(int amount)
    {
        stamina += amount;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        Debug.Log($"Stamina increased by {amount}. Current stamina: {stamina}");
    }
}
