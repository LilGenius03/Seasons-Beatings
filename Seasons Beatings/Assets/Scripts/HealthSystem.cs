using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 10f;
    public float FlyingHeadForce = 1f;
    float currenthealth;
    public int Deaths = 3;
    public float HammerDamage = 1f;
    public float dmgDelay = 1f;
    private bool IsDead = false;
    public GameObject BloodEffect;
    public GameObject SquirtEffect;
    private Rigidbody2D rb;

    public bool immune;
    public bool allowDamage = true;

    bool gameOver;
    [SerializeField] PlayerHandler handler;
    [SerializeField] private LayerMask deathLayers;
    [SerializeField] GameObject hammer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currenthealth = maxHealth;
        handler.scoreUIHandler.UpdateScores(Deaths);
    }

    IEnumerator DamageDelay()
    {
        immune = true;
        yield return new WaitForSeconds(dmgDelay);
        immune = false;
    }

    public void TakeDamage()
    {        
        if (immune || IsDead || !allowDamage)
            return;

        if(!GameManager.instance.gameOver)
            currenthealth -= HammerDamage;

        GameObject effect = Instantiate(BloodEffect, handler.head.transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        switch (currenthealth)
        {
            case 2f:
                handler.damageLight.SetActive(true);
                break;
            case 1f:
                handler.damageLight.SetActive(false);
                handler.damageHeavy.SetActive(true);
                break;
        }

        if (currenthealth <= 0 && IsDead == false)
        {
            Die();
        }
        StartCoroutine(DamageDelay());

    
    }

    public void Die(bool countTowardsDeaths = true)
    {
        IsDead = true;
        if(countTowardsDeaths)
            Deaths--;

        handler.scoreUIHandler.UpdateScores(Deaths);

        if (Deaths == 0)
        {
            hammer.SetActive(false);

            int newLayer = (int)Mathf.Log(deathLayers.value, 2);
            gameObject.layer = newLayer;

            //GameManager.instance.playersDead++;
            GameManager.instance.PlayerDied(handler);

            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            handler.freezedInputs = true;
            handler.head.gameObject.SetActive(false);

            FlyingHead();
        }
        else
            PlayerManager.instance.RespawnPlayer(handler.playerNum);
    }

    void FlyingHead()
    {            
        GameObject Squirt = Instantiate(SquirtEffect, handler.body.transform);
        Destroy(Squirt, 5f);
        GameObject FlyingHead = Instantiate(handler.currentCharacter.Flyinghead, handler.head.transform.position, handler.head.transform.rotation);
        FlyingHead.transform.localScale = handler.body.transform.localScale;
        FlyingHead.GetComponent<Rigidbody2D>().AddForce(Vector2.up * FlyingHeadForce, ForceMode2D.Impulse);
        FlyingHead.GetComponent<Rigidbody2D>().AddTorque(2, ForceMode2D.Impulse);
    }

    public void HealthReset()
    {
        handler.damageLight.SetActive(false);
        handler.damageHeavy.SetActive(false);
        hammer.SetActive(true);
        currenthealth = maxHealth;
        IsDead = false;
    }
}
