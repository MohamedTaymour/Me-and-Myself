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

    private bool relaxisJumping;
    private bool tensionisJumping;

    public bool RelaxisJumping 
    {get { return relaxisJumping; } set { relaxisJumping = value;}}

    public bool TensionisJumping
    {get { return tensionisJumping; } set { tensionisJumping = value;}}

    [Tooltip("Relax character's Physics")]
    [SerializeField] private Rigidbody2D RelaxedPlayer;

    [Tooltip("Tension character's Physics")]
    [SerializeField] private Rigidbody2D TensionedPlayer;

    [Header("Check if Both characters touch ground")]
    //Required Elements for isGrounded() check

    [Tooltip("Represents the feet dimensions for both players")]
    [SerializeField] private Vector2 boxSize;

    [Tooltip("Shows the feet position length")]
    [SerializeField] private float CastDistance;

    [Tooltip("Allows the game to identify what is the ground")]
    [SerializeField] private LayerMask Ground;

    [SerializeField] private AudioSource RelaxAudio;
    [SerializeField] private AudioSource TensionAudio;

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
    }

    public void RelaxJump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded(RelaxedPlayer))
        {
            RelaxisJumping = true;
            RelaxAudio.Play();
        }
    }

    public void TensionJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded(TensionedPlayer))
        {
            TensionisJumping = true;
            TensionAudio.Play();
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
