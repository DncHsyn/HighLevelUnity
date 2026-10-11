using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(
        EnemyAIController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enemy: DEAD");

        Object.Destroy(
            enemy.gameObject,
            2f);
    }

    public override void Tick()
    {
    }
}