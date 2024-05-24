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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(handler);
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
}
