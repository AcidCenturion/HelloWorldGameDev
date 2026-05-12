using UnityEngine;

public enum Level3BGM
{
    Morning,
    Chemistry,
    History,
    Lunch
}

public class MusicManager : MonoBehaviour
{


    private AudioSource[] audioSource;
    private AudioSource morningBGM;
    private AudioSource chemistryBGM;   
    private AudioSource historyBGM;
    private AudioSource lunchBGM;
    private AudioSource currBGM;


    private bool isPlaying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        morningBGM = audioSource[0];
        chemistryBGM = audioSource[1];
        historyBGM = audioSource[2];
        lunchBGM = audioSource[3];
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
            case Level3BGM.Chemistry:
                currBGM = chemistryBGM;
                break;
            case Level3BGM.History:
                currBGM = historyBGM;
                break;
            case Level3BGM.Lunch:
                currBGM = lunchBGM;
                break;
        }

        currBGM.Play();
        isPlaying = true;
    }

    
}
