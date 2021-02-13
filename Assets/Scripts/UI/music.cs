using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    // Start is called before the first frame update 
    public AudioSource audioSource;
    private float musicvolume = 1f;
   
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicvolume;
    } 
    public void updatevolume(float volume)
    {
        musicvolume = volume;
    }
}
