using UnityEngine;

public class RedStateManager : MonoBehaviour
{
    public RedState currentState;
    public Rigidbody2D redRB;

       //these four variables handle attacks
    public float radius;
    public LayerMask players;
    public GameObject attackPoint;
    [SerializeField] int attackDamage;

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

    public void CreateAttackHitbox()
    {
        
        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, players);

        foreach(Collider2D playerGameObject in player)
        {
            Debug.Log("HitEnemy");
            playerGameObject.GetComponent<L4Health>().health -= attackDamage;
        }


}
}

