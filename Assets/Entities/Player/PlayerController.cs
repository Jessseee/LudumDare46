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
        controls.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = controls.Player.Move.ReadValue<Vector2>() * 10;
    }
}
