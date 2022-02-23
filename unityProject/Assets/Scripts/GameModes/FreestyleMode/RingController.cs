using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    List<GameObject> propellers = new List<GameObject>();

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        // create a new material and give it a random color
        Material newMaterial = new Material(Shader.Find("Standard"));
        newMaterial.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        GameObject gate = transform.Find("Gate").gameObject;
        // Assign the new material to the object first slot
        {
            Material[] cachedMaterials = gate.GetComponent<Renderer>().materials;
            cachedMaterials[0] = newMaterial;
            gate.GetComponent<Renderer>().materials = cachedMaterials;
        }

        GameObject support = transform.Find("Support").gameObject;
        support.transform.rotation = Quaternion.Euler(-90, 0, transform.rotation.eulerAngles.y);
        {
            Material[] cachedMaterials = support.GetComponent<Renderer>().materials;
            cachedMaterials[1] = newMaterial;
            support.GetComponent<Renderer>().materials = cachedMaterials;
        }

        // Get all gameobjects children of the support
        for (int i = 0; i < support.transform.childCount; i++)
        {
            GameObject propeller = support.transform.GetChild(i).gameObject;
            {
                Material[] cachedMaterials = propeller.GetComponent<Renderer>().materials;
                cachedMaterials[1] = newMaterial;
                propeller.GetComponent<Renderer>().materials = cachedMaterials;
            }
            propellers.Add(propeller);
        }
    }

    private void Update()
    {
        // Rotate the propellers
        foreach (GameObject propeller in propellers)
        {
            propeller.transform.Rotate(20, 0, 0);
        }
    }
}
