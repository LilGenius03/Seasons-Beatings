using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Collision : MonoBehaviour
{
    ParticleSystem particle;
    public GameObject splatPrefab;
    public Transform splatHolder;
    private List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    public AudioSource audioSource;
    public AudioClip[] clips;
    public float SoundCapResetSpeed = 0.55f;
    public int MaxSounds = 3;
    float TimePassed;
    int soundsPlayed;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();   
    }

    // Update is called once per frame
    void Update()
    {
        TimePassed += Time.deltaTime;
        if(TimePassed > SoundCapResetSpeed)
        {
            soundsPlayed = 0;
            TimePassed = 0;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, colEvents);
        int count = colEvents.Count;

        for(int  i = 0; i < count; i++) 
        { 
          Instantiate(splatPrefab, colEvents[i].intersection, Quaternion.Euler(0f,0f, Random.Range(0f,360f)), splatHolder);
          
          if(soundsPlayed < MaxSounds)
            {
                soundsPlayed++;
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(clips[Random.Range(0,clips.Length)], Random.Range(0.1f, 0.35f));
            }  
        }
    }
}
