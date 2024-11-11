using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    private bool isMovingLeft;

    [Header("Patrol Points")]
    [SerializeField] private Transform RightPatrolPoint;
    [SerializeField] private Transform LeftPatrolPoint;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Speed")]
    [SerializeField] private float enemySpeed;

    void Start () {
        Debug.Log("Right Patrol Point: " + RightPatrolPoint.position);
        Debug.Log("Left Patrol Point: " + LeftPatrolPoint.position);
        Debug.Log("Enemy Start Position: " + enemy.position);
    }

    void Update () {
        if (isMovingLeft) {
            if (enemy.position.x >= LeftPatrolPoint.position.x) {
                Movement(-1);
            } else {
                ChangeDirection();
            }
        } else {
             if (enemy.position.x <= RightPatrolPoint.position.x) {
                 Movement(1);
             } else {
                 ChangeDirection();
             }
         }

    }

    void Movement (int direction) {
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * direction * enemySpeed, enemy.position.y);
    }

    void ChangeDirection () {
        isMovingLeft = !isMovingLeft;
    }
}
