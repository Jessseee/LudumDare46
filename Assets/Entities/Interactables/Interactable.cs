using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    public virtual void Interact() {
        Debug.LogWarning("Interact function not implemented!");
    }

    public virtual void ToggleUI(bool state)
    {

    }
}
