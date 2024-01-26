using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class RotateAround : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Transform point;

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position - point.position;

        float angle = rotationSpeed * Time.deltaTime * Mathf.Deg2Rad;
        Vector2 orbitPosition = new Vector2(
            currentPosition.x * Mathf.Cos(angle) - currentPosition.y * Mathf.Sin(angle),
            currentPosition.x * Mathf.Sin(angle) + currentPosition.y * Mathf.Cos(angle)
        );

        transform.position = new Vector3(orbitPosition.x, orbitPosition.y, transform.position.z) + point.position;
    }
}
