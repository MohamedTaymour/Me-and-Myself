using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float bounceMultiplier = 2f;

    public float maxHeight = 0;

    public Jumping jumping;

    private readonly int jumpPadLayer = 14;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if(jumping.IsGrounded(rb))
        {
            maxHeight = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == jumpPadLayer)
        {
            float incomingSpeed = Mathf.Abs(collision.relativeVelocity.y);

            if (maxHeight == 0)
                maxHeight = incomingSpeed * bounceMultiplier;
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxHeight);
        }
    }
}
