using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float attackTimer;

    public EnemyAttackState(
        EnemyAIController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enemy: ATTACK");

        enemy.StopMoving();
        attackTimer = 0f;
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
            enemy.ChangeState(enemy.SearchState);
            return;
        }

        if (!enemy.InAttackRange)
        {
            enemy.ChangeState(enemy.ChaseState);
            return;
        }

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            enemy.DamagePlayer();

            attackTimer = enemy.AttackCooldown;
        }
    }
}