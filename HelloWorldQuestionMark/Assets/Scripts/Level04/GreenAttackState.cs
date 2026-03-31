using UnityEngine;

public class GreenAttackState : GreenState
{
    public GreenChaseState chaseState;

    public override GreenState RunCurrentState()
    {
        if (!chaseState.isInRangeOfPlayer)
        {
            return chaseState;
        }
        else
        {
            Debug.Log("I have attacked!");
            return this;
        }


    }
}
