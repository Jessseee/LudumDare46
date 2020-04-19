using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionRadius : MonoBehaviour
{
    List<Interactable> interactables = new List<Interactable>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.gameObject.GetComponent<Interactable>();

        if (interactable != null)
        {
            interactables.Add(interactable);
            interactable.ToggleUI(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactable = collision.gameObject.GetComponent<Interactable>();
        interactables.Remove(interactable);
        interactable.ToggleUI(false);
    }

    public void Interact()
    {
        if (interactables.Count > 0)
        {
            Interactable interactable = interactables.Last();

            interactable.Interact();
        }
    }
}
