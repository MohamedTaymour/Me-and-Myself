using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class RopeSwing : MonoBehaviour
{
    [Header("References")]
    public RopeConstraint ropeConstraint;

    [Header("Pull Settings")]
    public float pullStrength = 150f;       // raised significantly from 20f
    public float pullArrivalThreshold = 1f;

    [Header("Rope Length Control")]
    public float ropeAdjustSpeed = 2f;
    public float minRopeLength = 1.5f;

    private bool powerupActive = false;
    private bool pendulumPhase = false;
    private Vector2 frozenPosition;
    private RigidbodyType2D originalBodyType;
    private float originalGravity;
    private float defaultMaxLength;         // stores default max length on start

    void Start()
    {
        // capture the default max length before any powerup changes it
        defaultMaxLength = ropeConstraint.maxLength;
    }

    public void OnActivate(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (!powerupActive)
            Activate();
        else
            Deactivate();
    }

    public void OnShorten(InputAction.CallbackContext context)
    {
        if (!pendulumPhase) return;
        if (context.performed)
        {
            ropeConstraint.maxLength = Mathf.Max(minRopeLength,
                ropeConstraint.maxLength - ropeAdjustSpeed * Time.deltaTime);
        }
    }

    public void OnLengthen(InputAction.CallbackContext context)
    {
        if (!pendulumPhase) return;
        if (context.performed)
        {
            ropeConstraint.maxLength += ropeAdjustSpeed * Time.deltaTime;
        }
    }

    void Activate()
    {
        powerupActive = true;
        pendulumPhase = false;

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

        // restore tension
        ropeConstraint.tension.bodyType = originalBodyType;
        ropeConstraint.tension.gravityScale = originalGravity;
        ropeConstraint.tension.position = frozenPosition;

        // snap rope back to default max length instantly
        ropeConstraint.maxLength = defaultMaxLength;
    }

    IEnumerator PullAndSwingRoutine()
    {
        // phase 1 — pull relax toward tension
        while (true)
        {
            ropeConstraint.tension.position = frozenPosition;
            ropeConstraint.tension.linearVelocity = Vector2.zero;

            Vector2 directionToTension = ((Vector2)ropeConstraint.tension.position
                                        - (Vector2)ropeConstraint.relax.position).normalized;
            float distanceToTension = Vector2.Distance(ropeConstraint.relax.position,
                                                       ropeConstraint.tension.position);

            // strong pull that scales with distance
            // multiply by both pullStrength and distanceToTension squared
            // squaring the distance makes the pull dramatically stronger the further away relax is
            ropeConstraint.relax.AddForce(directionToTension * (pullStrength * (distanceToTension * distanceToTension)));

            ropeConstraint.maxLength = distanceToTension;

            if (distanceToTension <= pullArrivalThreshold)
                break;

            yield return new WaitForFixedUpdate();
        }

        // phase 2 — pendulum
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