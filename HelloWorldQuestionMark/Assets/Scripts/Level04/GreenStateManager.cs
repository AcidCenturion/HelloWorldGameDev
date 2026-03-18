using UnityEngine;

public class GreenStateManager : MonoBehaviour
{
    public GreenState currentState;

    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        GreenState nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(GreenState nextState)
    {
        currentState = nextState;
    }
}
