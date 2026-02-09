using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Camera Section
    [Header("Camera")]
    [Tooltip("Camera Position")]
    [SerializeField] private Transform CameraPosition;

    //Importing sections from other code
    private Movement mv;

    void Start()
    {
        mv = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition.Translate(new Vector2(mv.MovementDirection * mv.MovementSpeed * Time.deltaTime, 0));
    }
}
