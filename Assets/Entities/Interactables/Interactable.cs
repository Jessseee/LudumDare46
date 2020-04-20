using TMPro;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    public string interactionText = "Interact";
    private TextMeshProUGUI interactionUI;

    private void Start()
    {
        interactionUI = GameObject.FindWithTag("InteractionUI").GetComponent<TextMeshProUGUI>();
        interactionUI.enabled = false;
    }

    public virtual void ToggleUI(bool state)
    {
        if (interactionUI == null) Debug.LogWarning("no interaciton UI set");
        interactionUI.enabled = state;
        interactionUI.text = "Press E to " + interactionText;
    }

    public virtual void Interact(PlayerController player)
    {
        Debug.LogWarning("Interact function not implemented!");
    }
}
