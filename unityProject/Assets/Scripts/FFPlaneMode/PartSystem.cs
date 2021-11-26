using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartSystem : MonoBehaviour
{
    public List<ParticleCollisionEvent> collisionEvents;
    public ParticleSystem part;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            part.Play();
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
