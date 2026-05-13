using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject coinPrefab;
    [Header("Drop Settings")]
    public int minCoins = 1;
    public int maxCoins = 3;
    public float dropChance = 0.75f; // 75% chance to drop coins

    [Header("Health")]
    public int health = 4; // Durability

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Break();
        }
    }

    public void Break()
    {
        // Roll for drop chance
        if (coinPrefab != null && Random.value < dropChance)
        {
            int coinCount = Random.Range(minCoins, maxCoins + 1);

            for (int i = 0; i < coinCount; i++)
            {
                // Spawn coin
                GameObject coin = Instantiate(
                    coinPrefab,
                    transform.position,
                    Quaternion.identity
                );

                // Pop effect
                Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(new Vector2(
                        Random.Range(-1.5f, 1.5f),  // horizontal scatter
                        Random.Range(3f, 5f)       // upward pop
                    ), ForceMode2D.Impulse);
                }
            }
        }
        Destroy(gameObject);
    }
}
