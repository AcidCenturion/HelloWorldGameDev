using UnityEngine;

public class BGMmanager : MonoBehaviour
{
    public AudioSource normalBGM;
    public AudioSource BossBGM;
    public PreBossCutScene preBossCutScene;
    public EnemyHealth enemyHealth;
    private bool triggersOnce = false;

    void Start()
    {
        normalBGM.Play();
    }

    [System.Obsolete]
    void Update()
    {
        if (enemyHealth.bossIsDead == true && !triggersOnce)
        {
            triggersOnce = true;
            Debug.Log("DEADDDD");

            //stops boss BGM (i couldnt find a better way to do this tbh hA)
            StopAllSceneAudio();
            normalBGM.Play();
        }
        
    }

    [System.Obsolete]
    public void StopAllSceneAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
    }


    //the PreBossCutscene BGM and Boss Fight BGM are managed by PreBossCutScene script rn. it's already working atm so
    // i will not pull it apart. Someone else do it if yall want but it does work i think 
}
