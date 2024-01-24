using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHazard : MonoBehaviour
{
    HealthSystem healthSystem;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem= Player.GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //healthSystem.HammerDamage++;
        }
    }
}
