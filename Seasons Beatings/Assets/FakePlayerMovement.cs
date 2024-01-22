using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector2 moveDirection;
    private Vector2 inputVector;

    private void Update()
    {
        moveDirection = new Vector2(inputVector.x, inputVector.y);
        moveDirection = transform.TransformDirection(moveDirection);


        //UnityEngine.Debug.Log(MoveDirection);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }
}
