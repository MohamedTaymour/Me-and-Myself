using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float MovementSpeed = 10f;

    private float MovementDirection;

    private float FaceDirection = 1;

    [Header("The Physics properties for the bodies")]

    [Tooltip("Relax character's Physics")]
    [SerializeField] private Rigidbody2D RelaxPlayer;

    [Tooltip("Tension character's Physics")]
    [SerializeField] private Rigidbody2D TensionedPlayer;

    [Header("Friction Properties for walls")]
    [SerializeField] private PhysicsMaterial2D wallFriction;

    [Header("Position of each body")]

    [Tooltip("Relax character's transformation")]
    [SerializeField] private Transform RelaxTransform;

    [Tooltip("Relax character's transformation")]
    [SerializeField] private Transform TensionTransform;


    // Update is called once per frame
    void Update()
    {
        RelaxPlayer.sharedMaterial = wallFriction;
        TensionedPlayer.sharedMaterial = wallFriction;
    }

    private void FixedUpdate()
    {
        RelaxPlayer.linearVelocityX = MovementDirection * MovementSpeed;

        TensionedPlayer.linearVelocityX = MovementDirection * MovementSpeed;
    }

    private void Flip()
    {
        FaceDirection *= -1;
        Vector2 Relaxscale = RelaxTransform.localScale;
        Vector2 Tensionscale = TensionTransform.localScale;

        Relaxscale.x *= -1;
        Tensionscale.x *= -1;

        RelaxTransform.localScale = Relaxscale;
        TensionTransform.localScale = Tensionscale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        float movementX = context.ReadValue<Vector2>().x;

        if (movementX < 0)
        {
            if (FaceDirection > 0)
                Flip();

            MovementDirection = -1;
        }

        else if (movementX > 0)
        {
            if(FaceDirection < 0)
                Flip();

            MovementDirection = 1;
        }
        else
        {
            MovementDirection = 0;
        }
    }
}
