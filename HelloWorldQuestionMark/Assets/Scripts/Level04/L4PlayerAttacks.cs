
using UnityEngine;
using UnityEngine.InputSystem;

public class L4PlayerAttacks : MonoBehaviour
{

    private int pComboIterator = 0;
    
    [SerializeField] int lightPDamage;
    public void Punch(InputAction.CallbackContext context)
    {
       Debug.Log("Punch!");
    }
}
