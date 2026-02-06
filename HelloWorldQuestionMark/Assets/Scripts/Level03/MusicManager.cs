using UnityEngine;

public enum Level3BGM
{
    Morning,
    Noon,
    Night
}

public class MusicManager : MonoBehaviour
{


    private AudioSource[] audioSource;
    private AudioSource morningBGM;
    private AudioSource noonBGM;   
    private AudioSource nightBGM;
    private AudioSource currBGM;


    private bool isPlaying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        morningBGM = audioSource[0];
        noonBGM = audioSource[1];
        nightBGM = audioSource[2];
    }

    public void PlayBGM(Level3BGM bgm)
    {

        if (currBGM != null && currBGM.isPlaying)
        {
            // Changing to same BGM -> skip
            if (audioSource[(int)bgm] == currBGM)
            {
                return;
            }

            currBGM.Stop();
        }

        switch (bgm)
        {
            case Level3BGM.Morning:
                currBGM = morningBGM;
                break;
            case Level3BGM.Noon:
                currBGM = noonBGM;
                break;
            case Level3BGM.Night:
                currBGM = nightBGM;
                break;
        }

        currBGM.Play();
        isPlaying = true;
    }

    
}
