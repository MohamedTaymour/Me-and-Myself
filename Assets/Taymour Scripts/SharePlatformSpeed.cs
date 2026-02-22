using System.Collections;
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
            StartCoroutine(SetParentNextFrame(other.transform, Platform));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetParentNextFrame(other.transform, parent));
        }
    }

    private IEnumerator SetParentNextFrame(Transform child, Transform newParent)
    {
        yield return null;
        if (child != null)
            child.SetParent(newParent);
    }
}