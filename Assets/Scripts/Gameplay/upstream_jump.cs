using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 25f;

    private void Start()
    {
        Debug.Log("JumpPad script is active!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("It's the Player!");

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Debug.Log("Jump applied!");
            }
        }
    }
}