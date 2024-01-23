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
    private Transform hammer;
    public float Rotationspeed;
    public float verticalSpeed;
    public float RetrackValue;
    private Vector2 MaxDistance = new Vector2(0, 1);

    
    private void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.Movement.HammerMovement.performed += ctx => rotationInput = ctx.ReadValue<float>();
        playerControls.Movement.HammerMovement.canceled += ctx => rotationInput = 0f;
        playerControls.Movement.RetrackHammer.performed += RetractHammer;
        playerControls.Movement.RetrackHammer.canceled += DetrackHammer;

      
    }

    

    private void OnEnable()
    {
        playerControls.Movement.Enable();
    }

    void RetractHammer(InputAction.CallbackContext ctx)
    {
        hammer.transform.position = new Vector2(0, RetrackValue);
    }

    void DetrackHammer(InputAction.CallbackContext ctx)
    {
        hammer.transform.position = Pivot.transform.position;
    }

    

    private void Update()
    {
        Vector2 movementInput = playerControls.Movement.HammerMovement.ReadValue<Vector2>();

        float angle = Mathf.Atan2(movementInput.x, movementInput.y) * Mathf.Rad2Deg;
        float rotationInput = movementInput.x;

        if(movementInput.magnitude > 0.1)
        {
            transform.eulerAngles = new Vector3(0f, 0f, -angle);
        }
        //transform.RotateAround(Pivot.position, Vector3.forward, -angle * Rotationspeed * Time.deltaTime);
        

        
        


        /*float verticalInput = movementInput.y;

        if (verticalInput >= MaxDistance.y)
        {
            verticalInput = movementInput.y;
        }

        transform.localPosition += Vector3.up * verticalInput * verticalSpeed * Time.deltaTime;*/


    }


}
