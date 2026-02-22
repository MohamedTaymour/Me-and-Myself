using UnityEngine;
using UnityEngine.InputSystem; // if using the new Input System

public class collision : MonoBehaviour
{
    public GameObject interactButton;   // assign in Inspector
    public GameObject dialoguePanel;    // assign in Inspector

    private bool playerInside = false;  // track if player is in the trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            if (UIManager.Instance != null && interactButton != null)
                UIManager.Instance.ShowInteractButton(interactButton);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (UIManager.Instance != null)
            {
                if (interactButton != null) UIManager.Instance.HideInteractButton(interactButton);
                if (dialoguePanel != null) UIManager.Instance.HideDialogue(dialoguePanel);
            }
        }
    }

    private void Update()
    {
        if (playerInside)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame) // press E to open dialogue
            {
                OnPressInteractButton();
            }
        }
    }

    public void OnPressInteractButton()
    {
        if (UIManager.Instance != null && dialoguePanel != null)
        {
            UIManager.Instance.ShowDialogue(dialoguePanel, interactButton);
        }
    }
}