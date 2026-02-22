using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInteractButton(GameObject button)
    {
        button.SetActive(true);
    }

    public void HideInteractButton(GameObject button)
    {
        button.SetActive(false);
    }

    public void ShowDialogue(GameObject panel, GameObject button = null)
    {
        panel.SetActive(true);
        if (button != null)
            button.SetActive(false);
    }

    public void HideDialogue(GameObject panel)
    {
        panel.SetActive(false);
    }
}