using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    PlayerControls playerControls;
    private float rotationInput;
    public Transform Pivot;
    public float Rotationspeed;
    public float verticalSpeed;

    
    private void Awake()
    {
        playerControls = new PlayerControls();
        Pivot = transform.parent;

        playerControls.Movement.HammerMovement.performed += ctx => rotationInput = ctx.ReadValue<float>();
        playerControls.Movement.HammerMovement.canceled += ctx => rotationInput = 0f;
    }

    

    private void OnEnable()
    {
        playerControls.Movement.Enable();
    }

    private void Update()
    {
        Vector2 movementInput = playerControls.Movement.HammerMovement.ReadValue<Vector2>();

        
        float rotationInput = movementInput.x;
        transform.RotateAround(Pivot.position, Vector3.forward, -rotationInput * Rotationspeed * Time.deltaTime);

        
        float verticalInput = movementInput.y;
        transform.localPosition += Vector3.up * verticalInput * verticalSpeed * Time.deltaTime;
    }


}
