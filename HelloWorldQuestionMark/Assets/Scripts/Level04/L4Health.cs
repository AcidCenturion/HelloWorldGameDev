using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class L4Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteRenderer spriteRenderer;
    [SerializeField] public int health;
    private int currentHealth;

    private bool blinkingRed = false;

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = health;      
    }

    void Update()
    {
        
        if (health < currentHealth)
        {
            StartCoroutine(BlinkRed());
            currentHealth = health;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    IEnumerator BlinkRed()
    {
        if (!blinkingRed)
        {
            blinkingRed = true;
            spriteRenderer.color = new Color(1.0f, 0.2f, 0.0f, 1.0f);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            blinkingRed = false;
        }   
    }
}
