using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private TextMeshProUGUI buttonText;

    // Define colors for different states
    public Color normalColor = Color.white;
    public Color highlightedColor = new (255f,88f,88f,255f);
    public Color pressedColor = new (103f,0,0,255);

    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.color = normalColor;
    }

    // On Hover Enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightedColor;
    }

    // On Hover Exit
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor;
    }

    // On Button Pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.color = pressedColor;
    }

    // On Button Released
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.color = highlightedColor;
    }
}