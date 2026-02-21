using UnityEngine;

public class LeverObject : MonoBehaviour
{
    public bool isOn = false;

    public Sprite spriteUp;
    public Sprite spriteDown;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Toggle()
    {
        isOn = !isOn;
        sr.sprite = isOn ? spriteUp : spriteDown;
    }
}