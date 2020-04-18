using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    InputController controls;

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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Add control delegates
        controls.Player.Interact.performed += ctx => handleInteract();
        controls.Player.Pause.performed += ctx => handlePause();
        controls.UI.Resume.performed += ctx => handleResume();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = controls.Player.Move.ReadValue<Vector2>() * 10;
    }

    void handleInteract()
    {

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
