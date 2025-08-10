using UnityEngine;

public class NarrVoiceIdlePlayer : MonoBehaviour
{  
    AudioSource source;
    private float seconds = 10;
    private float startTime;
    private bool hasPlayed = false;

    void Start() {
        source = GetComponent<AudioSource>();
        startTime = Time.time;
    }

    void Update() {
        if (!Input.anyKey)
        {
            if (seconds > 0)
            {
                seconds -= Time.deltaTime;
                // Debug.Log(seconds);
            }
            else if (seconds >= -1 && seconds <= 0 && hasPlayed == false)
            {
            // Debug.Log("go");
            source.Play();
            hasPlayed = true;
            }
        }
        else
        {
            seconds = -2;
        }
    }     
}
