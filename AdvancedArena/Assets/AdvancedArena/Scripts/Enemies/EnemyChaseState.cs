using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(
        EnemyAIController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enemy: CHASE");
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

        if (!enemy.CanSeePlayer)
        {
            enemy.ChangeState(enemy.IdleState);
            return;
        }

        if (enemy.InAttackRange)
        {
            enemy.ChangeState(enemy.AttackState);
            return;
        }

        enemy.MoveTowardsPlayer();
    }
}