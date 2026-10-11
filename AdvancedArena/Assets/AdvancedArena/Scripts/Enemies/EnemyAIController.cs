using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyAIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Settings")]
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float fleeHealthPercent = 0.3f;

    private Health health;
    private EnemyStateMachine stateMachine;

    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyFleeState FleeState { get; private set; }
    public EnemyDeadState DeadState { get; private set; }

    public Transform Player => player;
    public float AttackDamage => attackDamage;
    public float AttackCooldown => attackCooldown;

    public bool CanSeePlayer =>
        player != null &&
        Vector3.Distance(
            transform.position,
            player.position) <= detectionRange;

    public bool InAttackRange =>
        player != null &&
        Vector3.Distance(
            transform.position,
            player.position) <= attackRange;

    public bool ShouldFlee =>
        health.CurrentHealth > 0f &&
        health.CurrentHealth <=
        health.MaxHealth * fleeHealthPercent;

    public bool IsDead =>
        health.CurrentHealth <= 0f;

    private void Awake()
    {
        health = GetComponent<Health>();

        stateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        FleeState = new EnemyFleeState(this);
        DeadState = new EnemyDeadState(this);
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject foundPlayer =
                GameObject.FindGameObjectWithTag("Player");

            if (foundPlayer != null)
                player = foundPlayer.transform;
        }

        stateMachine.ChangeState(IdleState);
    }

    private void OnEnable()
    {
        if (health == null)
            health = GetComponent<Health>();

        health.Died += OnDied;
    }

    private void OnDisable()
    {
        if (health != null)
            health.Died -= OnDied;
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnDied()
    {
        stateMachine.ChangeState(DeadState);
    }

    public void ChangeState(IEnemyState state)
    {
        stateMachine.ChangeState(state);
    }

    public void MoveTowardsPlayer()
    {
        if (player == null)
            return;

        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        transform.position +=
            direction.normalized *
            moveSpeed *
            Time.deltaTime;
    }

    public void MoveAwayFromPlayer()
    {
        if (player == null)
            return;

        Vector3 direction =
            transform.position - player.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            direction = transform.forward;

        transform.position +=
            direction.normalized *
            moveSpeed *
            Time.deltaTime;
    }

    public void DamagePlayer()
    {
        if (!InAttackRange)
            return;

        Health playerHealth =
            player.GetComponent<Health>();

        if (playerHealth != null)
            playerHealth.TakeDamage(attackDamage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            transform.position, attackRange);
    }
}