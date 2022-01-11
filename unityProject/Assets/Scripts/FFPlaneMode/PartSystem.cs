using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartSystem : MonoBehaviour
{
    public List<ParticleCollisionEvent> collisionEvents;
    public ParticleSystem part;
    private bool send;
    public GameObject Player;
    private CanadaireObjectController canadaire;

    public GenerateFire flammegenerator;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        canadaire = Player.GetComponent<CanadaireObjectController>();
    }

    private void Update()
    {
        float waterlvl = canadaire.getWater();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!send && waterlvl > 0)
            {
                send = true;
                part.Play();
                part.enableEmission = true;

            }
            else if (send)
            {
                send = false;
                part.enableEmission = false;
            }
        }
        if (waterlvl > 0 && send == true)
        {
            canadaire.SetWater(waterlvl - 0.1f);
        }

        if (waterlvl < 0f)
        {
            canadaire.SetWater(0f);
            part.Stop();
            part.enableEmission = false;
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
                flammegenerator.nbflames--;
                Destroy(other.gameObject);
            }
            i++;
        }
    }
}
