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

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        Debug.Log(other.tag);


        while (i < numCollisionEvents)
        {
            if (other.tag == "Fire")
            {
                Debug.Log("je suis touch�");
                other.SetActive(false);
            }
            i++;
        }
    }
}
