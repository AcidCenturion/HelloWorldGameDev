using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour
{
    public Color targetHitColor;
    public SpriteRenderer targetRenderer;

    public float startValue = 0f;
    public float endValue = 100f;
    private readonly float changeAlphaDuration = 7f;
    public bool targetHit = false;
    public bool targetHitBlip = false;

    private float currentValue;
    private float actualValue;

    private Coroutine _LerpCoroutine;

    //DISCLAIMER I HAVE NO IDEA HOW THIS SCRIPT WORKS BUT IT TOOK SO LONG TO DO . 

    void Start()
    {
        targetRenderer = GetComponent<SpriteRenderer>();        
    }

    void Update()
    {
        //Debug.Log("actual value:" + actualValue);
        //Debug.Log("targetHit: " + targetHit);
        targetRenderer.color = new Color(1.0f, 0.2f, 0.0f, actualValue);

        if (actualValue == 0)
        {
            targetHit = false;
        }
        //Debug.Log("targethitblip: " + targetHitBlip);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Sword"))
        {
            if (targetHit)
            {
                //Debug.Log("targetHit true");
                RestartLerp();
            }
            else
            {
                StartLerp();
                targetHit = true;
                //Debug.Log("targetHit false");
            }
              
        }

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
        _LerpCoroutine = null;
        //Debug.Log("Final Value: " + actualValue);
    }

    void StartLerp()
    {
        if (_LerpCoroutine == null)
        {
            _LerpCoroutine = StartCoroutine(LerpAlphaValue(startValue, endValue, changeAlphaDuration));
            //Debug.Log("Lerp Started");
        }
    }

    void StopLerp()
    {
        if (_LerpCoroutine != null)
        {
            StopCoroutine(_LerpCoroutine);
            _LerpCoroutine = null;
            //Debug.Log("Lerp Stopped pt2");
        }
    }

    void RestartLerp()
    {
        StopLerp();
        StartLerp();
        //Debug.Log("Lerp Restarted");
    }

}
