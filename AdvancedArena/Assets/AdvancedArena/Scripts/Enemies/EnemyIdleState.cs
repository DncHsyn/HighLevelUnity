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
    }

    public override void Tick()
    {
        if (enemy.IsDead)
            return;

        if (enemy.ShouldFlee)
        {
            enemy.ChangeState(enemy.FleeState);
            return;
        }

        if (enemy.CanSeePlayer)
        {
            enemy.ChangeState(enemy.ChaseState);
        }
    }
}