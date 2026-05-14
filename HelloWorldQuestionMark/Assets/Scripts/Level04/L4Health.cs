using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class L4Health : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject DeathCanvas;
    public L4PlayerControls l4PlayerControls;
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
            if (this.CompareTag("Player"))
            {
                l4PlayerControls.enabled = false;
                DeathCanvas.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else
            {
                Destroy(gameObject);
            }
            
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
