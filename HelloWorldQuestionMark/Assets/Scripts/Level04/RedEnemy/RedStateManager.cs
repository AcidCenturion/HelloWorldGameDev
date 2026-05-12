using UnityEngine;

public class RedStateManager : MonoBehaviour
{
    public RedState currentState;
    public Rigidbody2D redRB;

    void Start()
    {
        redRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RunStateMachine();
        //Debug.Log(currentState);
    }

    private void RunStateMachine()
    {
        RedState RedNextState = currentState?.RunCurrentState();

        if (RedNextState != null)
        {
            SwitchToTheNextState(RedNextState);
        }
    }

    private void SwitchToTheNextState(RedState RedNextState)
    {
        currentState = RedNextState;
    }
}

