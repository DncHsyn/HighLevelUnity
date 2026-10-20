using UnityEngine;

public class EnemySearchState : EnemyState
{
    private float searchTimer;
    private bool reachedLastPosition;

    public EnemySearchState(
        EnemyAIController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enemy: SEARCH");

        searchTimer = enemy.SearchDuration;
        reachedLastPosition = false;

        enemy.MoveTo(enemy.LastKnownPosition);
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
            return;
        }

        if (!reachedLastPosition)
        {
            if (enemy.HasReachedDestination())
            {
                reachedLastPosition = true;
                enemy.StopMoving();
            }

            return;
        }

        searchTimer -= Time.deltaTime;

        if (searchTimer <= 0f)
        {
            enemy.ChangeState(enemy.PatrolState);
        }
    }

    public override void Exit()
    {
        enemy.StopMoving();
    }
}