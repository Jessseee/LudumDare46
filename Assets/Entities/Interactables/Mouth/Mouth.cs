using UnityEngine;

public class Mouth : Interactable
{
    int furnitureFed;
    Animator animator;
    CameraController camera;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        camera = Camera.main.GetComponent<CameraController>();
    }

    public override void Interact(PlayerController player)
    {
        if (player.grabbedItem != null)
        {
            Furniture grabbedItem = player.grabbedItem;

            grabbedItem.transform.SetParent(gameObject.transform);
            animator.SetTrigger("Eat");
            AudioController.instance.Play("Eating House");
            StartCoroutine(grabbedItem.MoveToParentCenter());
            furnitureFed += 1;
        }
    }

    public void Shake()
    {
        StartCoroutine(camera.Shake());
    }
}
