using TMPro;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    public string interactionText;
    public TextMeshProUGUI interactionUI;

    private void Start()
    {
        interactionUI.enabled = false;
    }

    public virtual void Interact() {
        Debug.LogWarning("Interact function not implemented!");
    }

    public virtual void ToggleUI(bool state)
    {
        if (interactionUI == null) Debug.LogWarning("no interaciton UI set");
        interactionUI.enabled = state;
        interactionUI.text = "Press E to " + interactionText;
    }
}
