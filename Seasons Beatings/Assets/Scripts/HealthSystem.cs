using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 10f;
    float currenthealth;
    public float HammerDamage = 1f;
    public float KnockBackForce = 1f;
    public float dmgDelay = 1f;
    private bool IsDead = false;
    public GameObject BloodEffect;
    private Rigidbody2D rb;
    bool immune;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currenthealth = maxHealth;
    }

    IEnumerator DamageDelay()
    {
        immune = true;
        yield return new WaitForSeconds(dmgDelay);
        immune = false;
    }

    public void TakeDamage(PlayerHandler otherPlayer)
    {
        if (immune)
            return;
        currenthealth -= HammerDamage;
        GameObject effect = Instantiate(BloodEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Vector2 difference = (gameObject.transform.position - transform.position).normalized;
        Vector2 force = difference * KnockBackForce;
        rb.AddForce(force, ForceMode2D.Impulse);
        Debug.Log("Ouch Man... That hurt");
        if (currenthealth <= 0 && IsDead == false)
        {
            Debug.Log("GameOver");
            GameManager.instance.IncreaseScore(otherPlayer.gameObject.GetComponent<PlayerHandler>().playerNum);
        }
        StartCoroutine(DamageDelay());
    }
}
