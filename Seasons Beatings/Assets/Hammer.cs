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
    public Transform hammer;
    public float Rotationspeed;
    public float verticalSpeed;
    public float RetrackValue;
    private Vector2 MaxDistance = new Vector2(0, 1);
    private Vector2 movementInput;
    private Vector2 ogPos;

    private void Awake()
    {
        ogPos = transform.localPosition;
    }

    public void RetractHammer()
    {
        hammer.transform.position = Pivot.transform.position;
    }

    public void DetrackHammer()
    {
        hammer.transform.position = ogPos;
    }

    public void SetInputVector(Vector2 input)
    {
        movementInput = input;
    }

    private void Update()
    {

        float angle = Mathf.Atan2(movementInput.x, movementInput.y) * Mathf.Rad2Deg;
        float rotationInput = movementInput.x;

        if(movementInput.magnitude > 0.1)
        {
            Pivot.transform.eulerAngles = new Vector3(0f, 0f, -angle);
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
