using UnityEngine;

public class SharePlatformSpeed : MonoBehaviour
{
    private Transform Platform;

    [SerializeField] private Transform parent;

    private void Start()
    {
        Platform = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(Platform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(parent);
        }
    }
}
