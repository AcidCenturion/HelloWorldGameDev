using UnityEngine;

public class GreenAttackState : GreenState
{
    public override GreenState RunCurrentState()
    {
        Debug.Log("I have attacked!");
        return this;
    }
}
