using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [Header("Options")]
    public float KnockBackForce = 1f;
    [Header("References")]
    [SerializeField]private Rigidbody2D rb;
    [SerializeField] float delay;
    bool isImune;
    
    public void PlayFeedback(GameObject sender)
    {
        if (isImune)
            return;
        Vector2 difference = (transform.position - sender.transform.position).normalized;
        Vector2 force = difference * KnockBackForce;
        rb.AddForce(force, ForceMode2D.Impulse);
        Debug.Log("Hit");
        StartCoroutine(KnockbackDelay());
    }

    IEnumerator KnockbackDelay()
    {
        isImune = true;
        yield return new WaitForSecondsRealtime(delay);
        isImune = false;
    }

}
