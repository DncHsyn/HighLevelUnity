public abstract class EnemyState : IEnemyState
{
    protected EnemyAIController enemy;

    protected EnemyState(
        EnemyAIController enemy)
    {
        this.enemy = enemy;
    }

    public virtual void Enter()
    {
    }

    public abstract void Tick();

    public virtual void Exit()
    {
    }
}