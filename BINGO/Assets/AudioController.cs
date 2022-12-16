using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip sound;
 
    AudioSource audioSource;
    
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayedSound(){
        audioSource.PlayOneShot(sound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
