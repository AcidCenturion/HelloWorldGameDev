using UnityEngine;

public class AffinityHolder : MonoBehaviour
{
    public static AffinityHolder Instance;

    public int RowanAffinity = 0;
    public int PerriAffinity = 0;
    public int GeminiAffinity = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // destroys dupe
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Sets instance 
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /*
        Called Everytime a player increases a companions affinity
        Affinities used in Boss
    */
    public void UpdateCharacterAffinity(Character.CharacterName character, int affinity)
    {
        Debug.Log("Updating Character affinity "+ character+ " with value " + affinity);
        switch (character)
        {
            case Character.CharacterName.Perri:
                PerriAffinity = affinity;
                break;
            case Character.CharacterName.Gemini:
                GeminiAffinity = affinity;
                break;
            case Character.CharacterName.Rowan:
                RowanAffinity = affinity;
                break;

        }
    }
}
