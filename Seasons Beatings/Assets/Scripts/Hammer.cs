using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    public Transform Pivot;
    public Transform hammer;
    public float hammerSpeed;
    public float RetrackValue;
    private Vector2 movementInput;
    public Transform ogPos;
    public bool retracting;
    public float retractSpeed;
    public Animator anim;
    [SerializeField] bool lerpedMovement;

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

    public void SetRetractValue(float input)
    {
        anim.SetFloat("Retract", input);
    }

    private void Update()
    {
        float angle = Mathf.Atan2(movementInput.x, movementInput.y) * Mathf.Rad2Deg;

        if(movementInput.magnitude > 0.1)
        {
            if(lerpedMovement)
                Pivot.transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(Pivot.transform.eulerAngles.z, -angle, hammerSpeed * Time.deltaTime));
            else
                Pivot.transform.eulerAngles = new Vector3(0f, 0f, -angle);
        }


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
    }


    

}
