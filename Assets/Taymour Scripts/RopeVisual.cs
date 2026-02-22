using UnityEngine;

public class RopeVisual : MonoBehaviour
{
    [Header("Rope Visualisation")]

    [Tooltip("Enter the physics of the rope")]
    public RopeConstraint ropeConstraint;

    [Tooltip("Enter the width of the rope")]
    public float lineWidth = 0.05f;

    [Tooltip("Enter the rate of shake intensity of the rope")]
    [Range(0f, 0.5f)] public float shakeIntensity = 0.05f; // Control how violent the shake is

    private LineRenderer lineRenderer;

    [Header("Players at each end of the rope")]
    [SerializeField] private Transform Relax;
    [SerializeField] private Transform Tension;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        // Using 3 points allows the middle of the rope to vibrate
        lineRenderer.positionCount = 3;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    void Update()
    {
        float currentDist = ropeConstraint.Distance;
        float maxDist = ropeConstraint.maxLength;
        float tensionPercent = currentDist / maxDist;

        Vector3 startPos = Tension.position;
        Vector3 endPos = Relax.position;
        Vector3 shakeOffset = Vector3.zero;

        // Check if we are past 70% tension
        if (tensionPercent >= 0.7f)
        {
            // Calculate intensity: starts at 0 at 70% length, reaches max at 100%
            float currentShakePower = Mathf.InverseLerp(0.7f, 1.0f, tensionPercent) * shakeIntensity;
            shakeOffset = Random.insideUnitSphere * currentShakePower;

            // Visual feedback: Lerp color from Blue (70%) to Red (100%)
            Color tensionColor = Color.Lerp(Color.blue, Color.red, Mathf.InverseLerp(0.7f, 1.0f, tensionPercent));
            lineRenderer.startColor = tensionColor;
            lineRenderer.endColor = tensionColor;
        }
        else
        {
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.blue;
        }

        // Apply positions
        lineRenderer.SetPosition(0, startPos);
        // The middle point gets the most shake for a better visual effect
        lineRenderer.SetPosition(1, Vector3.Lerp(startPos, endPos, 0.5f) + shakeOffset);
        lineRenderer.SetPosition(2, endPos);
    }
}