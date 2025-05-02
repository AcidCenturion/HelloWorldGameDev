using UnityEngine;

public class Breakable : MonoBehaviour
{
  //      ---Special Notes---
  //  How to Apply:
  //  Script applied to Root of Breakable object
  //  Breakable objects should contain another gameObject within that contains a 2DCollider w/ isTrigger set true
  //  Parent must contain a 2D Collider & 2D Rigidbody (kinematic) Component
  //  Assign breakable objects the "Breakable" tag
  //  
  //  Only broken by pellets (and boss??)
 


  [SerializeField] private int durability = 1;

    //Method called by pellets 
    public void DecreaseDurability(int dmg)
    {
        durability -= dmg;

        if (durability <= 0)
        {
            //Destroys the breakable object
            Destroy(gameObject);
        }
    }

    //      ---Possible Ideas---
    //in boss' script, it should have an OnTriggerEnter2D method that checks if object tag is "Breakable" and then destroy the breakable object
    //OR the boss script will make the breakable objects collider inactive and turn its transparency to max, and then after a few seconds, collider is active again and transparency is back to normal
    
    

}
