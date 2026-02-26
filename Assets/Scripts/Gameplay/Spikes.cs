using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DeathScreen.TriggerDeath();
            Debug.Log("Spikes Enterd");
        }
    }
}
