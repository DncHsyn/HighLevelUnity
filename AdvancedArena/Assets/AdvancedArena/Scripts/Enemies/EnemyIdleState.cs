using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(
        EnemyAIController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enemy: IDLE");
        enemy.StopMoving();
    }

    public override void Tick()
    {
        if (enemy.CanSeePlayer)
        {
            enemy.ChangeState(enemy.ChaseState);
        }
    }
}