using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnComptuerInteract : MonoBehaviour
{
    private bool canInteract = false;
    private readonly int ComputerMask = 7;

    private readonly int RelaxMask = 8;
    private readonly int TensionMask = 9;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == ComputerMask)
        {
            if (gameObject.layer == RelaxMask)
                Debug.Log("Relax enter");
            else if (gameObject.layer == TensionMask)
                Debug.Log("Tension enter");
                
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.layer == RelaxMask)
            Debug.Log("Relax exit");
        else if (gameObject.layer == TensionMask)
            Debug.Log("Tension exit");

        canInteract = false;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (canInteract && context.performed)
        {
            Debug.Log("Yes");
            StartCoroutine(InteractionCooldown());
        }
    }

    private IEnumerator InteractionCooldown()
    {
        if (canInteract)
        {
            canInteract = false;
            yield return new WaitForSeconds(1.5f);
            canInteract = true;
        }
    }

}
