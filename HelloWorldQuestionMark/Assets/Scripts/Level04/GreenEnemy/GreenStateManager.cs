using UnityEngine;

public class GreenStateManager : MonoBehaviour
{
    public GreenState currentState;
    public Rigidbody2D greenRB;

    //these four variables handle attacks
    public float radius;
    public LayerMask players;
    public GameObject attackPoint;
    [SerializeField] int attackDamage;

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

    public void CreateAttackHitbox()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, players);

        foreach(Collider2D playerGameObject in player)
        {
            //Debug.Log("HitEnemy");
            playerGameObject.GetComponent<L4Health>().health -= attackDamage;
        }

    }
}

