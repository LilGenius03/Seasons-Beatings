using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 10f;
    public float FlyingHeadForce = 1f;
    float currenthealth;
    private int Deaths = 3;
    public float HammerDamage = 1f;
    public float dmgDelay = 1f;
    private bool IsDead = false;
    public GameObject BloodEffect;
    private Rigidbody2D rb;
    bool immune;
    bool gameOver;
    [SerializeField] PlayerHandler handler;
    public KnockBack knockBack;
    [SerializeField] private LayerMask deathLayers;

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

    public void TakeDamage(PlayerHandler otherPlayer)
    {        
        knockBack.PlayFeedback(otherPlayer.gameObject);
        if (immune)
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
            IsDead = true;
            Deaths--;
            handler.scoreUIHandler.UpdateScores(Deaths);
            if (Deaths == 0)
            {
                int newLayer = (int)Mathf.Log(deathLayers.value, 2);
                gameObject.layer = newLayer;
                GameManager.instance.playersDead++;
                GameManager.instance.IncreaseScore(otherPlayer.gameObject.GetComponent<PlayerHandler>().playerNum, handler.playerNum);
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                Collider2D col = GetComponent<Collider2D>();
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                handler.freezedInputs = true;
                col.enabled = false;
                handler.head.gameObject.SetActive(false);
                GameObject FlyingHead = Instantiate(handler.currentCharacter.Flyinghead, handler.body.transform);
                FlyingHead.GetComponent<Rigidbody2D>().AddForce(transform.up * FlyingHeadForce, ForceMode2D.Impulse);
                knockBack.gameObject.SetActive(false);
            }
            else
                PlayerManager.instance.ResetPlayers(handler.playerNum);
        }
        StartCoroutine(DamageDelay());

    
    }

    public void HealthReset()
    {
        handler.damageLight.SetActive(false);
        handler.damageHeavy.SetActive(false);
        currenthealth = maxHealth;
        IsDead = false;
    }
}
