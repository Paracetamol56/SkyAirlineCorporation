using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSound : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> Clips;
    private AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        int randIndex = Random.Range(0, Clips.Count);
        audiosource.clip = Clips[randIndex];
        audiosource.Play();
        StartCoroutine(DestroyInstance());
    }

    IEnumerator DestroyInstance()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
