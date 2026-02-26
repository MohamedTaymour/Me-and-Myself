using UnityEngine;
using UnityEngine.InputSystem;

public class HowToPlay : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject mainMenuPanel;

    void Start()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (howToPlayPanel != null) howToPlayPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        if (howToPlayPanel != null) howToPlayPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    void Update()
    {
        if (howToPlayPanel == null) return;
        if (Keyboard.current.escapeKey.wasPressedThisFrame && howToPlayPanel.activeSelf)
            ClosePanel();
    }
}