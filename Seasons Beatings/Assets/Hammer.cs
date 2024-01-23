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
    public Transform ogPos;
    public bool retracting;
    public float retractSpeed;

    public void RetractHammer()
    {
        retracting = true;
    }

    public void DetrackHammer()
    {
        retracting = false;
    }

    public void SetInputVector(Vector2 input)
    {
        movementInput = input;
    }

    private void FixedUpdate()
    {




        //hammer.transform.position = Mathf.Lerp(hammer.position, ogPos.position)
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


        if (retracting)
        {
            float x = Mathf.Lerp(hammer.position.x, Pivot.transform.position.x, retractSpeed);
            float y = Mathf.Lerp(hammer.position.y, Pivot.transform.position.y, retractSpeed);
            hammer.transform.position = new Vector2(x, y);
        }
        else
        {
            float x = Mathf.Lerp(hammer.position.x, ogPos.transform.position.x, retractSpeed * 2);
            float y = Mathf.Lerp(hammer.position.y, ogPos.transform.position.y, retractSpeed * 2);
            hammer.transform.position = new Vector2(x, y);
        }



        /*float verticalInput = movementInput.y;

        if (verticalInput >= MaxDistance.y)
        {
            verticalInput = movementInput.y;
        }

        transform.localPosition += Vector3.up * verticalInput * verticalSpeed * Time.deltaTime;*/


    }


}
