using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartSystem : MonoBehaviour
{
    public List<ParticleCollisionEvent> collisionEvents;
    public ParticleSystem part;
    private bool send;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!send)
            {
                send = true;
                part.Play();
                part.enableEmission = true;
            }
            else
            {
                send = false;
                part.enableEmission = false;
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;


        while (i < numCollisionEvents)
        {
            if (other.tag == "Fire")
            {
                other.SetActive(false);
            }
            i++;
        }
    }
}
