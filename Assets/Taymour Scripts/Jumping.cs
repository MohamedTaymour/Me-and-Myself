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
    private bool canJump;

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

    [Tooltip("Determines the Audio Source of the assets")]
    AudioSource JumpAudio;

    [Tooltip("Determines the min max values of Pitch")]
    [SerializeField] Vector2 AudioPitch = new(0.9f,1.1f);
    public void Start()
    {
        JumpAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if(canJump)
        {
            RelaxedPlayer.linearVelocityY = JumpForce;
            TensionedPlayer.linearVelocityY = JumpForce;
            canJump = false;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        float random = Random.Range(AudioPitch.x,AudioPitch.y);
        if(context.performed && IsGrounded())
        {
            canJump = true;
            JumpAudio.pitch = random;
            JumpAudio.Play();
        }   
    }

    public bool IsGrounded()
    {
        Vector2 relaxGround = new (RelaxedPlayer.position.x, RelaxedPlayer.position.y - CastDistance);
        Vector2 tensionGround = new (TensionedPlayer.position.x, TensionedPlayer.position.y - CastDistance);

        if (Physics2D.OverlapBox(relaxGround,boxSize,0,Ground) && Physics2D.OverlapBox(tensionGround, boxSize, 0, Ground))
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
