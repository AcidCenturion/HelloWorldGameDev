using UnityEngine;
public class MusicChanger : MonoBehaviour
{
    public static MusicChanger instance;
    private AudioSource music01, music02;
    private bool isPlayingMusic;
    public AudioClip musicClip;
    void Start()
    {
        music01 = gameObject.AddComponent<AudioSource>();
        music02 = gameObject.AddComponent<AudioSource>();
        isPlayingMusic = true;
        ChangeMusic(musicClip);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void ChangeMusic(AudioClip newMusic)
    {
        if (isPlayingMusic == music01)
        {
            music02.clip = newMusic;
            music02.Play();
            music01.Stop();
        }
        if (isPlayingMusic == music02)
        {
            music01.clip = newMusic;
            music01.Play();
            music02.Stop();
        }
        isPlayingMusic = !isPlayingMusic;
    }
    public void ReturnInitial ()
    {
        ChangeMusic(musicClip);
    }
}
