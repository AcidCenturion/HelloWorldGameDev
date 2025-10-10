using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour
{
    public Color redColor = Color.red;
    public SpriteRenderer targetRenderer;

    private float changeAlpha = 10f;
    private bool targetHit = false;

    // UNFINISHED
    // Currently, target turns red when sword touches it. Next to work on is making it so the red fades over some number of seconds 

    void Start()
    {
        targetRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Sword"))
        {
            targetRenderer.color = redColor;
            targetHit = true;
            Debug.Log("red!");
        }
        else
        {
            Debug.Log("didnt work");
        }
    }

    IEnumerator TenSecondTimer()
    {
        //yield return new WaitForSeconds(10);
    }
}
