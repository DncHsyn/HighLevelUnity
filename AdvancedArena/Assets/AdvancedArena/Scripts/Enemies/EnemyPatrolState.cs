using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private int currentPointIndex;

    public EnemyPatrolState(
        EnemyAIController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enemy: PATROL");

        MoveToCurrentPoint();
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

        if (enemy.HasReachedDestination())
        {
            currentPointIndex++;

            if (currentPointIndex >=
                enemy.PatrolPoints.Length)
            {
                currentPointIndex = 0;
            }

            MoveToCurrentPoint();
        }
    }

    private void MoveToCurrentPoint()
    {
        if (enemy.PatrolPoints.Length == 0)
            return;

        Transform point =
            enemy.PatrolPoints[currentPointIndex];

        if (point != null)
            enemy.MoveTo(point.position);
    }

    public override void Exit()
    {
        enemy.StopMoving();
    }
}