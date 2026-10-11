using UnityEngine;

public class EnemyFleeState : EnemyState
{
    public EnemyFleeState(
        EnemyAIController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enemy: FLEE");
    }

    public override void Tick()
    {
        if (enemy.IsDead)
            return;

        if (!enemy.ShouldFlee)
        {
            enemy.ChangeState(enemy.IdleState);
            return;
        }

        if (enemy.CanSeePlayer)
        {
            enemy.MoveAwayFromPlayer();
        }
    }
}