using UnityEngine;
using System.Collections;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance;

    void Awake() => instance = this;

    public static Coroutine Run(IEnumerator routine)
    {
        return instance.StartCoroutine(routine);
    }
}