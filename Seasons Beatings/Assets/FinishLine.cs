using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishLine : MonoBehaviour
{
    RaceToTheTopMode mm;

    private void Start()
    {
        mm = ModeManager.instance.GetComponent<RaceToTheTopMode>();
        Debug.Log(mm.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mm.PlayerFinished(collision.GetComponent<PlayerInput>());
        }
    }
}
