using UnityEngine;

public class PlayIntroNarratorAudio : MonoBehaviour
{
    
    public AudioSource source;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source.Play();
    }
}
