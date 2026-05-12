using UnityEngine;

public enum Level3BossMusic
{
    REGULAR,
    PLAYER_LOW,
    BOSS_LOW
}

public class Level3BossSceneMusic : MonoBehaviour
{
    private AudioSource[] musics;
    private AudioSource BGM;
    private AudioSource lowPlayerHealthBGM;
    private AudioSource lowBossHealthBGM;
    private AudioSource currMusic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musics = GetComponents<AudioSource>();
        BGM = musics[0];
        lowPlayerHealthBGM = musics[1];
        lowBossHealthBGM = musics[2];

        if (BGM) BGM.Play();
        currMusic = BGM;
    }

    public void PlayLevel3BossMusic(Level3BossMusic bgm)
    {
        if (currMusic != null && currMusic.isPlaying)
        {
            if (musics[(int)bgm] == currMusic)
            {
                return;
            }

            currMusic.Stop();
        }

        switch(bgm)
        {
            case Level3BossMusic.REGULAR:
                currMusic = BGM;
                break;
            case Level3BossMusic.PLAYER_LOW:
                currMusic = lowPlayerHealthBGM;
                break;
            case Level3BossMusic.BOSS_LOW:
                currMusic = lowBossHealthBGM;
                break;
        }

        currMusic.Play();
    }
}
