using UnityEngine;

public class Resettable : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        RespawnManager.Register(this);
    }

    void OnDestroy()
    {
        RespawnManager.Unregister(this);
    }

    public void ResetToStart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        gameObject.SetActive(true);
    }
}