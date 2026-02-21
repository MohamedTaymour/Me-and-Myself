using UnityEngine;
using UnityEngine.InputSystem;

public class LeverPull : MonoBehaviour
{
    public bool nearLever = false;

    private LeverObject currentLever;

    private int leverLayer = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == leverLayer)
        {
            nearLever = true;
            currentLever = collision.GetComponent<LeverObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nearLever = false;
        currentLever = null;
    }

    public void Flip(InputAction.CallbackContext context)
    {
        if (nearLever && context.performed && currentLever != null)
        {
            currentLever.Toggle();
        }
    }
}