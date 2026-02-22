using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 25f;
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        if (audioManager == null)
            Debug.LogError("AudioManager not found!");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                if(audioManager!= null)
                {
                    audioManager.Play("UpStream");
                }
            }
        }
    }
}