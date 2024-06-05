using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PlayerHandler handler;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip hit;
    [SerializeField] float soundDelay;
    bool canPlaySound = true;

    public float KnockBackForce = 1f;

    [SerializeField] float knockbackDelay;
    bool isImune;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem otherPlayer = collision.gameObject.GetComponent<HealthSystem>();
            Knockback(otherPlayer);
            otherPlayer.TakeDamage();

            if (canPlaySound)
            {
                source.PlayOneShot(hit);
                StartCoroutine(SoundDelay());
            }

        }
    }

    IEnumerator SoundDelay()
    {
        canPlaySound = false;
        yield return new WaitForSecondsRealtime(1);
        canPlaySound = true;
    }

    public void Knockback(HealthSystem sender)
    {
        if (sender.immune)
            return;
        Vector2 difference = (sender.transform.position - transform.position).normalized;
        Debug.Log(difference);
        Vector2 force = difference * KnockBackForce;
        sender.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        Debug.Log("Hit");
        StartCoroutine(KnockbackDelay());
    }

    IEnumerator KnockbackDelay()
    {
        isImune = true;
        yield return new WaitForSecondsRealtime(KnockBackForce);
        isImune = false;
    }
}
