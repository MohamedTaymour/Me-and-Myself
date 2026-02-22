using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public GameObject interactButton;
    public GameObject dialoguePanel;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInteractButton()
    {
        interactButton.SetActive(true);
    }

    public void HideInteractButton()
    {
        interactButton.SetActive(false);
    }

    public void ShowDialogue()
    {
        dialoguePanel.SetActive(true);
        HideInteractButton();
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}