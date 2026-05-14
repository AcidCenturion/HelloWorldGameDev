using UnityEngine;

public class BGMusicManager : MonoBehaviour
{
    public AudioSource streetBGM;
    public AudioSource combatBGM;
    public L4CutsceneManager l4CutsceneManager;

    void Start()
    {
        
    }

    void Update()
    {
        ChangeBGM();
    }

    void ChangeBGM()
    {
        if (l4CutsceneManager.inCutscene)
        {
            streetBGM.enabled = true;
            combatBGM.enabled = false;
        }
        else if (!l4CutsceneManager.inCutscene)
        {
            streetBGM.enabled = false;
            combatBGM.enabled = true;
        }
    }
}
