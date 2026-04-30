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
        //Debug.Log(currentState);
    }

    private void RunStateMachine()
    {
        GreenState GreenNextState = currentState?.RunCurrentState();

        if (GreenNextState != null)
        {
            SwitchToTheNextState(GreenNextState);
        }
    }

    private void SwitchToTheNextState(GreenState GreenNextState)
    {
        currentState = GreenNextState;
    }
}

