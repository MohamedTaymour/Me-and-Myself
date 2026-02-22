using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Potential Controllers")]
    public ButtonPress button;
    public LeverObject lever;

    [Header("Target Points")]
    public Transform Top;
    public Transform Bottom;
    public Transform Platform;

    [Header("Settings")]
    public float speed = 2f;

    private Transform currentTarget;
    private bool requiresButton;
    private bool requiresLever;

    private bool IsActivated()
    {
        if (requiresButton) return button.isHeld;
        if (requiresLever) return lever.isOn;
        return true;
    }

    void Start()
    {
        currentTarget = Top;
        requiresButton = button != null;
        requiresLever = lever != null;
    }

    void Update()
    {
        if (currentTarget == null) return;
        if (!IsActivated()) return;

        Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, currentTarget.position, speed * Time.deltaTime);

        if (Vector3.Distance(Platform.transform.position, currentTarget.position) < 0.01f)
        {
            Platform.transform.position = currentTarget.position;
            currentTarget = (currentTarget == Top) ? Bottom : Top;
        }
    }

    
}