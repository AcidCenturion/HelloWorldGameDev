using UnityEngine;

public class GreenStateManager : MonoBehaviour
{
    public GreenState currentState;
    public Rigidbody2D greenRB;

    void Start()
    {
        greenRB = GetComponent<Rigidbody2D>();
    }

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
