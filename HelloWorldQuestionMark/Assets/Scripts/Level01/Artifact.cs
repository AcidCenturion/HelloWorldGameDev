using UnityEngine;

public class Artifact : MonoBehaviour
{
    public bool artifactCollected = false;
    public L1LevelWin l1LevelWin;


    void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.CompareTag("Player"))
        {
            artifactCollected = true;
            Destroy(this.gameObject);
        }
    }
}
