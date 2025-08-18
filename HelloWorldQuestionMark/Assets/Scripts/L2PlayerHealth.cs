using UnityEngine;

public class L2PlayerHealth : MonoBehaviour
{
    public int Health = 3;


    void Start()
    {
        
    }

    void Update()
    {
        
        if (Health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Health --;
        }
        //Debug.Log("health: " + Health);
    }
}
