using UnityEngine;
using UnityEngine.InputSystem;

public class HowToPlay : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject mainMenuPanel;

    void Start()
    {
        // Make sure panel is closed at start
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        if (howToPlayPanel != null) howToPlayPanel.SetActive(true);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
    }

    public void ClosePanel()
    {
        if (howToPlayPanel != null) howToPlayPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    void Update()
    {
        if (howToPlayPanel == null) return; // prevent null error
        if (Keyboard.current.escapeKey.wasPressedThisFrame && howToPlayPanel.activeSelf)
            ClosePanel();
    }
}