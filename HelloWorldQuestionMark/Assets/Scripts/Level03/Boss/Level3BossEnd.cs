using UnityEngine;

public class Level3BossEnd : MonoBehaviour
{

    public static Level3BossEnd Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
