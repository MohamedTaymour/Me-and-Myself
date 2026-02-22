using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Jumping : MonoBehaviour
{
    //required elements for jumping

    [Header("Jump Mechanics")]
    
    [Tooltip("Determine both character's jump force")]
    public float JumpForce = 10f;

    [Tooltip("Determine gravity effect on rigid bodies")]
    public float Gravity = 1f;

    private bool RelaxisJumping;
    private bool TensionisJumping;

    [Tooltip("Relax character's Physics")]
    public Rigidbody2D RelaxedPlayer;

    [Tooltip("Tension character's Physics")]
    public Rigidbody2D TensionedPlayer;

    [Header("Check if Both characters touch ground")]
    //Required Elements for isGrounded() check

    [Tooltip("Represents the feet dimensions for both players")]
    public Vector2 boxSize;

    [Tooltip("Shows the feet position length")]
    public float CastDistance;

    [Tooltip("Allows the game to identify what is the ground")]
    [SerializeField] private LayerMask Ground;



    void FixedUpdate()
    {
        if(RelaxisJumping)
        {
            RelaxedPlayer.linearVelocityY = JumpForce;
            RelaxisJumping = false;
        }

        if (TensionisJumping)
        {
            TensionedPlayer.linearVelocityY = JumpForce;
            TensionisJumping = false;
        }

        if(!IsGrounded(TensionedPlayer))
        {
            TensionedPlayer.linearVelocityY -= Gravity;
        }

        if (!IsGrounded(RelaxedPlayer))
        {
            RelaxedPlayer.linearVelocityY -= Gravity;
        }
    }

    public void RelaxJump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded(RelaxedPlayer))
        {
            RelaxisJumping = true;
        }
    }

    public void TensionJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded(TensionedPlayer))
        {
            TensionisJumping = true;
        }
    }

    public bool IsGrounded(Rigidbody2D rb)
    {
        Vector2 rbGround = new (rb.position.x, rb.position.y - CastDistance);

        if (Physics2D.OverlapBox(rbGround,boxSize,0,Ground))
            return true;
        else
            return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(RelaxedPlayer.position.x, RelaxedPlayer.position.y - CastDistance), boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(TensionedPlayer.position.x, TensionedPlayer.position.y - CastDistance), boxSize);
    }
}
