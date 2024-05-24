using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.instance.ResetPlayers(collision.gameObject.GetComponent<PlayerHandler>().playerNum, true);
        }
    }
}
