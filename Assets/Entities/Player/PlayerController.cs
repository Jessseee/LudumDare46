using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public List<Interactable> interactables = new List<Interactable>();
    public Furniture grabbedItem;

    Rigidbody2D rb;
    InputController controls;
    Animator animator;
    SpriteRenderer sprite;

    private void Awake()
    {
        controls = new InputController();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controls.UI.Disable();
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        controls.Player.Disable();
        controls.UI.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        //Add control delegates
        controls.Player.Interact.performed += ctx => handleInteract();
        controls.Player.Pause.performed += ctx => handlePause();
        controls.UI.Resume.performed += ctx => handleResume();
    }

    void Update()
    {
        Vector2 direction = controls.Player.Move.ReadValue<Vector2>();
        rb.velocity = direction * 10;

        if (direction != Vector2.zero)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (direction.y < 0)
        {
            animator.SetBool("Backward", true);
        }

        if (direction.y > 0)
        {
            animator.SetBool("Backward", false);
        }

        if (direction.x < 0)
        {
            sprite.flipX = true;
        }

        if (direction.x > 0)
        {
            sprite.flipX = false;
        }
    }

    void handleInteract()
    {
        if (interactables.Count > 0)
        {
            Interactable interactable = interactables.Last();
            interactable.Interact(this);
        } else if (grabbedItem)
        {
            grabbedItem.transform.parent = null;
            grabbedItem = null;
        }
    }

    void handlePause()
    {
        this.enabled = false;
    }

    void handleResume()
    {
        this.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Interactable interactable = collider.GetComponent<Interactable>();

        if (interactable != null)
        {
            interactables.Add(interactable);
            interactable.ToggleUI(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Interactable interactable = collider.GetComponent<Interactable>();

        if (interactable != null)
        {
            interactables.Remove(interactable);
            interactable.ToggleUI(false);
        }
    }
}
