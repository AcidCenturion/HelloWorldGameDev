using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Vector2 respawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        Debug.Log("Respawned! Health reset.");
    }
}
