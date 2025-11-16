using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public TargetScript Target1;
    public TargetScript Target2;
    public TargetScript Target3;
    public TargetScript Target4;

    private bool targetsCompleted = false;

    void Update()
    {
        if (Target1.targetHit && Target2.targetHit && Target3.targetHit && Target4.targetHit) //if all 4 targets are hit, run TargetsComplete func
        {
            if (targetsCompleted == false) // this makes it so the TargetsComplete func only runs once
            {
                TargetsComplete();
            }
        }
    }

    public void TargetsComplete()
    {
        Debug.Log("bonk");
        targetsCompleted = true;
    }
}
