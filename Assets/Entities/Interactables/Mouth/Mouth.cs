using UnityEngine;

public class Mouth : Interactable
{
    int furnitureFed;
    Animator animator;
    ParticleSystem particles;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        particles = GetComponent<ParticleSystem>();
    }

    public override void Interact(PlayerController player)
    {
        if (player.grabbedItem != null)
        {
            Furniture grabbedItem = player.grabbedItem;

            grabbedItem.transform.SetParent(gameObject.transform);
            animator.SetTrigger("Eat");
            StartCoroutine(grabbedItem.MoveToParentCenter());
            furnitureFed += 1;
        }
    }
}
