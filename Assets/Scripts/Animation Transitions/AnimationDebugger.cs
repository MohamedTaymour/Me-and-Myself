using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationDebugger : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private PlayerState currentState;

    private bool isGrounded;
    private bool wasGrounded;

    private float moveInput;

    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public enum PlayerType { Relax, Tension }
    public PlayerType playerType;
    private readonly float jumpCooldown = 0f;
    private float lastJumpTime;
    private bool jumpedThisFrame;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogError("ANIMATOR IS NULL on " + gameObject.name);
        else
            Debug.Log("Animator found: " + animator.runtimeAnimatorController);

        ChangeState(PlayerState.Idle);
        //rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

        //ChangeState(PlayerState.Idle);
    }

    void Update()
    {
        HandleInput();
        HandleStateLogic();
    }
    bool CanJump()
{
    return isGrounded 
        && Time.time > lastJumpTime + jumpCooldown 
        && currentState != PlayerState.Land
        && currentState != PlayerState.Jump
        && currentState != PlayerState.Falling;
}
    void HandleInput()
    {
        moveInput = 0;
        if (playerType == PlayerType.Relax)
        {
            if (Keyboard.current.aKey.isPressed) moveInput = -1;
            else if (Keyboard.current.dKey.isPressed) moveInput = 1;

            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (Keyboard.current.wKey.wasPressedThisFrame && isGrounded && CanJump()
     && Time.time > lastJumpTime + jumpCooldown
     && currentState != PlayerState.Land) // ADD THIS
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                lastJumpTime = Time.time;
                
                jumpedThisFrame = true;
            }
        }
        else if (playerType == PlayerType.Tension)
        {
            if (Keyboard.current.jKey.isPressed) moveInput = -1;
            else if (Keyboard.current.lKey.isPressed) moveInput = 1;

            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (Keyboard.current.iKey.wasPressedThisFrame && isGrounded && CanJump()
     && Time.time > lastJumpTime + jumpCooldown
     && currentState != PlayerState.Land) // ADD THIS
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                lastJumpTime = Time.time;
                
                jumpedThisFrame = true;
            }
        }
    }

    void HandleStateLogic()
    {
        jumpedThisFrame = true;
        isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.1f;

        if (!isGrounded && rb.linearVelocity.y > 0 && jumpedThisFrame)
        {
            jumpedThisFrame = false;
            ChangeState(PlayerState.Jump);
        }
        else if (!isGrounded && rb.linearVelocity.y <= 0)
            ChangeState(PlayerState.Falling);
        else if (!wasGrounded && isGrounded)
            ChangeState(PlayerState.Land);
        else if (isGrounded && Mathf.Abs(moveInput) > 0.1f)
            ChangeState(PlayerState.Moving);
        else if (isGrounded)
            ChangeState(PlayerState.Idle);

        wasGrounded = isGrounded;

        // ADD THESE — update animator every frame
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetFloat("VelocityY", rb.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded);
    }

    void ChangeState(PlayerState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
        Debug.Log("Current State: " + currentState);

        // Animator connection
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("isGrounded", isGrounded);

        if (newState == PlayerState.Land)
        {
            animator.SetTrigger("Land");
        }
    }
}

public enum PlayerState
{
    Idle,
    Moving,
    Jump,
    Falling,
    Land
}
