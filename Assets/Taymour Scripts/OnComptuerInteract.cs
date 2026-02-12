using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnComptuerInteract : MonoBehaviour
{
    private bool canInteract = false;

    private int PlayerMask = 8;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == PlayerMask)
        {
            Debug.Log("enter");
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == PlayerMask)
        {
            Debug.Log("exit");
            canInteract = false;
        }
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
        canInteract = false;
        yield return new WaitForSeconds(1.5f);
        canInteract = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(1, 1));
    }
}
