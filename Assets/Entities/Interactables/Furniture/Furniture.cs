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
        }
    }

    public override void ToggleUI(bool state)
    {
        interactionText = "Furniture";
        base.ToggleUI(state);
    }
    IEnumerator Move(float delayTime, Vector3Int endPos)
    {
        yield return new WaitForSeconds(delayTime); // start at time X
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= 1)
        { // until one second passed
            transform.position = Vector3.Lerp(transform.position, endPos, Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
    }
}
