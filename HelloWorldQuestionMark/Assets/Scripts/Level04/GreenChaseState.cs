using UnityEngine;

public class GreenChaseState : GreenState
{
    public bool isInRangeOfPlayer;
    public GreenStateManager greenStateManager;
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

    void Update()
    {
        Vector3 temp = new (1.0f, 0, 0);
        greenStateManager.greenRB.transform.position = temp;
    }

    public void InRangeOfPlayer()
    {
        
    }


}
