using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    PlayerControls playerControls;
    private Vector2 moveVector;
    public Transform Pivot;
    private Rigidbody2D rb;
    public float Rotationspeed;

    private void FixedUpdate()
    {
        transform.RotateAround(Pivot.transform.position, Vector3.forward, moveVector.x * Rotationspeed * Time.fixedDeltaTime);

        rb.velocity = transform.up * moveVector.y;
    }
    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();

        playerControls.Movement.HammerMovement.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        playerControls.Movement.HammerMovement.canceled += ctx => moveVector = Vector2.zero;
    }

    

    private void OnEnable()
    {
        playerControls.Movement.Enable();
    }


}
