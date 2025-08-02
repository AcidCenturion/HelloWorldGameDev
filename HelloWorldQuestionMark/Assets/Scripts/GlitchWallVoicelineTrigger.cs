using UnityEngine;

public class GlitchWallVoicelineTrigger : MonoBehaviour
{
    AudioSource source;
    Collider2D soundTrigger;
    private bool hasPlayed = false;

    void Awake() {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player" && !source.isPlaying && !hasPlayed)
        {
            source.Play();
            hasPlayed = true;
        }
    }

}