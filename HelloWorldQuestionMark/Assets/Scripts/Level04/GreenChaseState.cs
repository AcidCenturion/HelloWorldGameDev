using UnityEngine;

public class GreenChaseState : GreenState
{
    public bool isInRangeOfPlayer;
    public GreenAttackState attackState;

    public override GreenState RunCurrentState()
    {
        if (isInRangeOfPlayer)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
