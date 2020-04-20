using TMPro;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    public string interactionText = "Interact";
    public SpriteRenderer sprite;
    private TextMeshProUGUI interactionUI;

    public virtual void Start()
    {
        interactionUI = GameObject.FindWithTag("InteractionUI").GetComponent<TextMeshProUGUI>();
        interactionUI.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
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
