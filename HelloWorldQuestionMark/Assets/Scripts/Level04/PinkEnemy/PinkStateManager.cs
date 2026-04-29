using UnityEngine;

public class PinkStateManager : MonoBehaviour
{
    public PinkState currentState;
    public Rigidbody2D pinkRB;

    void Start()
    {
        pinkRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RunStateMachine();
        //Debug.Log(currentState);
    }

    private void RunStateMachine()
    {
        PinkState nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(PinkState nextState)
    {
        currentState = nextState;
    }
}

