using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private PlayerState currentState;

    private bool isGrounded;
    private bool wasGrounded;

    private float moveInput;

    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ChangeState(PlayerState.Idle);
    }

    void Update()
    {
        HandleInput();
        HandleStateLogic();
    }

    void HandleInput()
    {
        moveInput = 0;

        if (Keyboard.current.aKey.isPressed)
            moveInput = -1;
        else if (Keyboard.current.dKey.isPressed)
            moveInput = 1;

        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jump
        if (Keyboard.current.wKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void HandleStateLogic()
    {
        isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.01f;

        if (!isGrounded && rb.linearVelocity.y > 0)
        {
            ChangeState(PlayerState.Jumping);
        }
        else if (!isGrounded && rb.linearVelocity.y < 0)
        {
            ChangeState(PlayerState.Falling);
        }
        else if (!wasGrounded && isGrounded)
        {
            ChangeState(PlayerState.Landing);
        }
        else if (Mathf.Abs(moveInput) > 0.1f)
        {
            ChangeState(PlayerState.Moving);
        }
        else
        {
            ChangeState(PlayerState.Idle);
        }

        wasGrounded = isGrounded;
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

        if (newState == PlayerState.Landing)
        {
            animator.SetTrigger("Land");
        }
    }
}

public enum PlayerState
{
    Idle,
    Moving,
    Jumping,
    Falling,
    Landing
}
