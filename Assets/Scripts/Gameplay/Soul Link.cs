using UnityEngine;

public class RopeConstraint : MonoBehaviour
{
    [Header("Players at each end of the Rope")]
    public Rigidbody2D tension;
    public Rigidbody2D relax;

    [Tooltip("Max length for the bodies to be away from one another")]
    public float maxLength = 5f;

    private float _distance;
    public float Distance 
    { set { _distance = value; } get { return _distance; } }

    private Vector2 _direction;
    public Vector2 Direction
    { get { return _direction; } set { _direction = value; } }

    void FixedUpdate()
    {
        Vector2 posA = tension.position;
        Vector2 posB = relax.position;

        Distance = Vector2.Distance(posA, posB);

        if (Distance >= maxLength)
        {
            Direction = (posB - posA).normalized;
            float excess = Distance - maxLength;

            // Move both bodies equally toward each other
            Vector2 correction = Direction * (excess / 2f);

            tension.position += correction;
            relax.position -= correction;

            // Remove separating velocity
            RemoveSeparatingVelocity(Direction);
        }
    }

    void RemoveSeparatingVelocity(Vector2 direction)
    {
        float velA = Vector2.Dot(tension.linearVelocity, direction);
        float velB = Vector2.Dot(relax.linearVelocity, -direction);

        if (velA < 0)
            tension.linearVelocity -= direction * velA;

        if (velB > 0)
            relax.linearVelocity += direction * velB;
    }
}