using UnityEngine;

public class EnemyFleeState : EnemyState
{
    private float fleeTimer;

    public EnemyFleeState(
        EnemyAIController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enemy: FLEE");

        fleeTimer = 0f;
    }

    public override void Tick()
    {
        if (enemy.IsDead)
            return;

        if (!enemy.ShouldFlee)
        {
            enemy.ChangeState(enemy.PatrolState);
            return;
        }

        fleeTimer -= Time.deltaTime;

        if (fleeTimer <= 0f)
        {
            enemy.FleeFromPlayer();
            fleeTimer = 1f;
        }
    }

    public override void Exit()
    {
        enemy.StopMoving();
    }
}