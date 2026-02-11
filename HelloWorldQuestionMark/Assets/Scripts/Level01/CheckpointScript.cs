using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private Animator anim;
    private AudioSource source;

    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                playerRespawn.SetCheckpoint(transform.position);  // Update the latest checkpoint
                Debug.Log("Checkpoint Reached!");
            }

            anim.SetBool("ReachedCheckpoint", true);
            source.Play();
        }
    }
}
