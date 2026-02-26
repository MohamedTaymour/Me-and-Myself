using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class RopeSwing : MonoBehaviour
{
    [Header("References")]
    public RopeConstraint ropeConstraint;
    public Jumping jumping;

    [Header("Pull Settings")]
    public float pullStrength = 150f;
    public float pullArrivalThreshold = 1f;

    [Header("Rope Length Control")]
    public float ropeAdjustSpeed = 2f;
    public float minRopeLength = 1.5f;

    public bool powerupActive = false;
    public bool pendulumPhase = false;
    private Vector2 frozenPosition;
    private RigidbodyType2D originalBodyType;
    private float originalGravity;
    private float defaultMaxLength;

    // store held state of shorten and lengthen
    private bool isShorteningRope = false;
    private bool isLengtheningRope = false;

    void Start()
    {
        defaultMaxLength = ropeConstraint.maxLength;
    }

    void Update()
    {
        if (!pendulumPhase) return;

        // apply rope length change every frame while button is held
        if (isShorteningRope)
        {
            ropeConstraint.maxLength = Mathf.Max(minRopeLength,
                ropeConstraint.maxLength - ropeAdjustSpeed * Time.deltaTime);
        }

        if (isLengtheningRope)
        {
            ropeConstraint.maxLength += ropeAdjustSpeed * Time.deltaTime;
        }
    }

    public void OnActivate(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!jumping.IsGrounded(ropeConstraint.tension)) return;

        if (!powerupActive)
            Activate();
        else
            Deactivate();
    }

    public void OnShorten(InputAction.CallbackContext context)
    {
        if (!pendulumPhase) return;

        // started means button pressed down, canceled means button released
        if (context.started)
            isShorteningRope = true;
        else if (context.canceled)
            isShorteningRope = false;
    }

    public void OnLengthen(InputAction.CallbackContext context)
    {
        if (!pendulumPhase) return;

        if (context.started)
            isLengtheningRope = true;
        else if (context.canceled)
            isLengtheningRope = false;
    }

    void Activate()
    {
        powerupActive = true;
        pendulumPhase = false;
        isShorteningRope = false;
        isLengtheningRope = false;

        originalBodyType = ropeConstraint.tension.bodyType;
        originalGravity = ropeConstraint.tension.gravityScale;

        frozenPosition = ropeConstraint.tension.position;
        ropeConstraint.tension.bodyType = RigidbodyType2D.Kinematic;
        ropeConstraint.tension.linearVelocity = Vector2.zero;

        StartCoroutine(PullAndSwingRoutine());
    }

    void Deactivate()
    {
        StopAllCoroutines();
        powerupActive = false;
        pendulumPhase = false;
        isShorteningRope = false;
        isLengtheningRope = false;

        ropeConstraint.tension.bodyType = originalBodyType;
        ropeConstraint.tension.gravityScale = originalGravity;
        ropeConstraint.tension.position = frozenPosition;

        ropeConstraint.maxLength = defaultMaxLength;
    }

    IEnumerator PullAndSwingRoutine()
    {
        while (true)
        {
            ropeConstraint.tension.position = frozenPosition;
            ropeConstraint.tension.linearVelocity = Vector2.zero;

            Vector2 directionToTension = ((Vector2)ropeConstraint.tension.position
                                        - (Vector2)ropeConstraint.relax.position).normalized;
            float distanceToTension = Vector2.Distance(ropeConstraint.relax.position,
                                                       ropeConstraint.tension.position);

            ropeConstraint.relax.AddForce(directionToTension * (pullStrength * (distanceToTension * distanceToTension)));
            ropeConstraint.maxLength = distanceToTension;

            if (distanceToTension <= pullArrivalThreshold)
                break;

            yield return new WaitForFixedUpdate();
        }

        pendulumPhase = true;
        ropeConstraint.relax.AddForce(new Vector2(3f, 0f), ForceMode2D.Impulse);

        while (powerupActive)
        {
            ropeConstraint.tension.position = frozenPosition;
            ropeConstraint.tension.linearVelocity = Vector2.zero;
            yield return new WaitForFixedUpdate();
        }
    }
}