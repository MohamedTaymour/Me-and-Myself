using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float MovementSpeed = 10f;

    private float RelaxMovementDirection;

    private float TensionMovementDirection;

    private float RelaxFaceDirection = 1;

    private float TensionFaceDirection = 1;

    [Header("The Physics properties for the bodies")]

    [Tooltip("Relax character's Physics")]
    [SerializeField] private Rigidbody2D RelaxedPlayer;

    [Tooltip("Tension character's Physics")]
    [SerializeField] private Rigidbody2D TensionedPlayer;

    [Header("Friction Properties for walls")]
    [SerializeField] private PhysicsMaterial2D wallFriction;

    [Header("Position of each body")]

    [Tooltip("Relax character's transformation")]
    [SerializeField] private Transform RelaxTransform;

    [Tooltip("Tension character's transformation")]
    [SerializeField] private Transform TensionTransform;

    [Header("Audio Options")]
    [Tooltip("Relax character's movement audio")]
    [SerializeField] private AudioSource RelaxMovementAudio;

    [Tooltip("Tension character's movement audio")]
    [SerializeField] private AudioSource TensionMovementAudio;

    private AudioManager audioManager;
    private Jumping jumping;

    private void Start()
    {
        jumping = GetComponent<Jumping>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RelaxedPlayer.sharedMaterial = wallFriction;
        TensionedPlayer.sharedMaterial = wallFriction;
    }

    private void FixedUpdate()
    {
        RelaxedPlayer.linearVelocityX = RelaxMovementDirection * MovementSpeed;

        TensionedPlayer.linearVelocityX = TensionMovementDirection * MovementSpeed;
    }

    private void FlipRelaxed()
    {
        RelaxFaceDirection *= -1;
        Vector2 Relaxscale = RelaxTransform.localScale;
        Relaxscale.x *= -1;
        RelaxTransform.localScale = Relaxscale;

    }

    private void FlipTensioned()
    {
        TensionFaceDirection *= -1;
        Vector2 Tensionscale = TensionTransform.localScale;
        Tensionscale.x *= -1;
        TensionTransform.localScale = Tensionscale;
    }

    public void MoveRelax(InputAction.CallbackContext context)
    {
        float movementX = context.ReadValue<Vector2>().x;

        if (movementX < 0)
        {
            if (RelaxFaceDirection > 0)
                FlipRelaxed();

            RelaxMovementDirection = -1;

            if (jumping.IsGrounded(RelaxedPlayer))
            {
                audioManager.PlayLoop("R_walk");
            }
        }

        else if (movementX > 0)
        {
            if (RelaxFaceDirection < 0)
                FlipRelaxed();

            RelaxMovementDirection = 1;

            if (jumping.IsGrounded(RelaxedPlayer))
            {
                audioManager.PlayLoop("R_walk");
            }
        }
        else
        {
            RelaxMovementDirection = 0;

            audioManager.Stop("R_walk");
        }
    }

    public void MoveTension(InputAction.CallbackContext context)
    {
        float movementX = context.ReadValue<Vector2>().x;

        if (movementX < 0)
        {
            if (TensionFaceDirection > 0)
                FlipTensioned();

            TensionMovementDirection = -1;

            if (jumping.IsGrounded(TensionedPlayer))
            {
                audioManager.PlayLoop("T_walk");
            }
        }

        else if (movementX > 0)
        {
            if (TensionFaceDirection < 0)
                FlipTensioned();

            TensionMovementDirection = 1;

            if (jumping.IsGrounded(TensionedPlayer))
            {
                audioManager.PlayLoop("T_walk");
            }
        }
        else
        {
            TensionMovementDirection = 0;

            audioManager.Stop("T_walk");
        }
    }
}
