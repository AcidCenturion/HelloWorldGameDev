using UnityEngine;

public class L1SoundEffectManager : MonoBehaviour
{
    public static L1SoundEffectManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySoundEffect(AudioClip clip, Vector3 position)
    {
        GameObject soundObject = new GameObject("SoundEffect");
        soundObject.transform.position = position;
        AudioSource source = soundObject.AddComponent<AudioSource>();

        source.clip = clip;
        source.Play();
    
        Destroy(soundObject, clip.length);
    }

}
