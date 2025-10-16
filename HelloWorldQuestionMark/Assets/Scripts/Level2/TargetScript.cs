using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour
{
    public Color targetHitColor;
    public SpriteRenderer targetRenderer;

    public float startValue = 0f;
    public float endValue = 100;
    private float changeAlphaDuration = 10f;
    private bool targetHit = false;

    private float currentValue;
    private float actualValue;

    // UNFINISHED
    // Currently, target turns orange when sword touches it, and slowly fades to transparent over 10seconds
    // Next step is to make it so object glows and doesnt become transparent

    void Start()
    {
        targetRenderer = GetComponent<SpriteRenderer>();   
    }

    void Update()
    {
        //Debug.Log("actualValue:" + actualValue);
        targetRenderer.color = new Color(1.0f, 0.78f, 0.0f, actualValue);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Sword"))
        {
            targetRenderer.color = new Color(1.0f, 0.78f, 0.0f, 1);
            StartCoroutine(LerpAlphaValue(startValue, endValue, changeAlphaDuration));
            targetHit = true;
            Debug.Log("hit!");
            StartCoroutine(WaitTenSeconds());
            
        }
        // else
        // {
        //     Debug.Log("didnt work");
        // }
    }

    IEnumerator LerpAlphaValue(float fromValue, float toValue, float lerpDuration)
    {
        float timer = 0f;

        while (timer < lerpDuration)
        {
            timer += Time.deltaTime;
            float t = timer / lerpDuration;

            currentValue = Mathf.Lerp(fromValue, toValue, t); //currentValue is a value that goes from 0 to 1 over the span of 10s (changeAlphaDuration)
            actualValue = 1 - currentValue; //makes a value that goes from 1 to 0

            //Debug.Log("actualValue" + actualValue);

            yield return null;
        }

        currentValue = toValue;
        //Debug.Log("Final Value: " + actualValue);
    }

    IEnumerator WaitTenSeconds()
    {
        yield return new WaitForSeconds(10f);
        targetHit = false;
        Debug.Log("reset!");
    }
}
