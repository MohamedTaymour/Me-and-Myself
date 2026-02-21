using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public bool isHeld = false;

    public Sprite ButtonUp;

    public Sprite ButtonDown;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = ButtonUp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHeld = true;

        sr.sprite = ButtonUp;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHeld = false;

        sr.sprite = ButtonDown;
    }
}
