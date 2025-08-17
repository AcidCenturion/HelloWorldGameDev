using UnityEngine;

public class CanBreakPots : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Pot"))
        {
            other.GetComponent<PotBreaks>().Broken();
        }
    }
}
