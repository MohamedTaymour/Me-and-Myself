using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance;

    [Header("Death Screen")]
    public GameObject DeathComponent;
    public Image redOverlay;
    public float fadeInDuration = 0.6f;
    public float holdDuration = 0.5f;
    public float fadeOutDuration = 0.8f;

    [SerializeField] private PlayerInput playerInput;
    private GameObject coroutineRunner;

    void Awake()
    {
        Instance = this;
        coroutineRunner = new GameObject("DeathScreenRunner");
        DontDestroyOnLoad(coroutineRunner);
        coroutineRunner.AddComponent<CoroutineRunner>();
    }

    void Start()
    {
        DeathComponent.SetActive(false);
    }

    public static void TriggerDeath()
    {
        CoroutineRunner.Run(Instance.DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        playerInput.DeactivateInput();

        DeathComponent.SetActive(true);
        SetAlpha(0f);

        yield return CoroutineRunner.Run(Fade(0f, 1f, fadeInDuration));
        yield return new WaitForSecondsRealtime(holdDuration);

        RespawnManager.ResetAll();

        yield return CoroutineRunner.Run(Fade(1f, 0f, fadeOutDuration));

        DeathComponent.SetActive(false);
        playerInput.ActivateInput();
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            SetAlpha(Mathf.Lerp(from, to, Mathf.Clamp01(elapsed / duration)));
            yield return null;
        }
        SetAlpha(to);
    }

    private void SetAlpha(float alpha)
    {
        Color c = redOverlay.color;
        c.a = alpha;
        redOverlay.color = c;
    }
}