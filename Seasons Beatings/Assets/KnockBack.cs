using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [Header("Options")]
    public float KnockBackForce = 1f;
    public float delay = 0.15f;
    [Header("References")]
    private Rigidbody2D rb;
    public UnityEvent OnStart, OnDone;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnStart.Invoke();
        Vector2 difference = (transform.position - sender.transform.position).normalized;
        Vector2 force = difference * KnockBackForce;
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(Reset());

    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        OnDone.Invoke();
    }
}
