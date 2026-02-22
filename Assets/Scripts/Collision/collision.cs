using UnityEngine;

public class SpecialBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowInteractButton();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.HideInteractButton();
            UIManager.Instance.HideDialogue(); // 👈 Add this
        }
    }
}