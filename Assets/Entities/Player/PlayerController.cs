using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    InputController controls;
    Animator animator;
    SpriteRenderer sprite;
    InteractionRadius interactionRadius;

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
        interactionRadius = GetComponentInChildren<InteractionRadius>();

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
        interactionRadius.Interact();
    }

    void handlePause()
    {
        this.enabled = false;
    }

    void handleResume()
    {
        this.enabled = true;
    }
}
