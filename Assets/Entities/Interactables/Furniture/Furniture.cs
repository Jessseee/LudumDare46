using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : Interactable
{
    public override void Interact(PlayerController player)
    {
        if (player.grabbedItem == null)
        {
            transform.parent = player.transform;
            player.grabbedItem = this;
            player.interactables.Remove(this);
        }
    }

    public override void ToggleUI(bool state)
    {
        interactionText = "grab furniture";
        base.ToggleUI(state);
    }

    float currentLerpTime = 0.0f;
    float lerpTime = 1.5f;
    public IEnumerator MoveToParentCenter()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        while (currentLerpTime < lerpTime)
        {
            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = t*t*t * (t * (6f * t - 15f) + 10f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, t);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), t);
            sprite.color = Color.Lerp(sprite.color, Color.clear, t/10);

            yield return 1;
        }
        Destroy(gameObject);
    }
}
