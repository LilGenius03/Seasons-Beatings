using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float Playerhealth = 10f;
    public float HammerDamage = 1f;
    private bool IsDead = false;
    public GameObject BloodEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Hammer"))
        {
            Playerhealth -= HammerDamage;
            Instantiate(BloodEffect, transform.position, Quaternion.identity);
            Destroy(BloodEffect, 1f);

            if (Playerhealth <= 0 && IsDead == true)
            {
                Debug.Log("GameOver");
                GameManager.instance.IncreaseScore(collision.gameObject.GetComponent<PlayerHandler>().playerNum);             
            }
        }


    }
}
