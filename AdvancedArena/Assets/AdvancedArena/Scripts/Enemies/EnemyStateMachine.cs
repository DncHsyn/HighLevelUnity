public class EnemyStateMachine
{
    private IEnemyState currentState;

    public void ChangeState(IEnemyState newState)
    {
        if (currentState == newState)
            return;

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Tick();
        }
    }
}