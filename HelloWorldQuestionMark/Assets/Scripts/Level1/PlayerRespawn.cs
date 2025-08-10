using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Vector2 respawnPoint;
    public GameObject boss; //For Boss cutscene
                            // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bgm;
    private void Start()
    {
        respawnPoint = transform.position; // Initial spawn position
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void SetCheckpoint(Vector2 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }

    public void Respawn()
    {
        transform.position = respawnPoint; // Move player to the last checkpoint
        if (playerHealth != null)
        {
            playerHealth.ResetHealth(); // Reset health using PlayerHealth script when respawning
        }
        if (boss != null)
        {
            boss.GetComponent<PreBossCutScene>().enabled = true;
        }
        if (bgm != null)
        {
            bgm.GetComponent<AudioSource>().Play();
        }
        Debug.Log("Respawned! Health reset.");
    }
}
