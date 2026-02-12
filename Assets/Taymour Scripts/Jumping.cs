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

    private bool RelaxcanJump;
    private bool TensioncanJump;

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

    [Tooltip("Determines the Relax Player Audio Source of the assets")]
    [SerializeField] private AudioSource RelaxJumpAudio;

    [Tooltip("Determines the Tension Player Audio Source of the assets")]
    [SerializeField] private AudioSource TensionJumpAudio;

    [Tooltip("Determines the min max values of Pitch")]
    [SerializeField] Vector2 AudioPitch = new(0.9f,1.1f);

    void FixedUpdate()
    {
        if(RelaxcanJump)
        {
            RelaxedPlayer.linearVelocityY = JumpForce;
            RelaxcanJump = false;
        }

        if (TensioncanJump)
        {
            TensionedPlayer.linearVelocityY = JumpForce;
            TensioncanJump = false;
        }
    }

    public void RelaxJump(InputAction.CallbackContext context)
    {
        float random = Random.Range(AudioPitch.x,AudioPitch.y);
        if(context.performed && IsGrounded(RelaxedPlayer))
        {
            RelaxcanJump = true;
            RelaxJumpAudio.pitch = random;
            RelaxJumpAudio.Play();
        }   
    }

    public void TensionJump(InputAction.CallbackContext context)
    {
        float random = Random.Range(AudioPitch.x, AudioPitch.y);
        if (context.performed && IsGrounded(TensionedPlayer))
        {
            TensioncanJump = true;
            TensionJumpAudio.pitch = random;
            TensionJumpAudio.Play();
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
