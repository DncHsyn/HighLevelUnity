using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyVision))]
public class EnemyAIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] patrolPoints;

    [Header("Combat")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1.5f;

    [Header("AI Settings")]
    [SerializeField] private float searchDuration = 4f;
    [SerializeField] private float fleeHealthPercent = 0.3f;

    private Health health;
    private EnemyVision vision;
    private NavMeshAgent agent;
    private EnemyStateMachine stateMachine;

    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemySearchState SearchState { get; private set; }
    public EnemyFleeState FleeState { get; private set; }
    public EnemyDeadState DeadState { get; private set; }

    public Transform Player => player;
    public NavMeshAgent Agent => agent;
    public Transform[] PatrolPoints => patrolPoints;
    public float AttackCooldown => attackCooldown;
    public float SearchDuration => searchDuration;

    public Vector3 LastKnownPosition { get; private set; }

    public bool CanSeePlayer =>
        vision.CanSee(player);

    public bool InAttackRange =>
        player != null &&
        Vector3.Distance(
            transform.position,
            player.position) <= attackRange;

    public bool IsDead =>
        health.CurrentHealth <= 0f;

    public bool ShouldFlee =>
        !IsDead &&
        health.CurrentHealth <=
        health.MaxHealth * fleeHealthPercent;

    private void Awake()
    {
        health = GetComponent<Health>();
        vision = GetComponent<EnemyVision>();
        agent = GetComponent<NavMeshAgent>();

        stateMachine = new EnemyStateMachine();

        PatrolState = new EnemyPatrolState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        SearchState = new EnemySearchState(this);
        FleeState = new EnemyFleeState(this);
        DeadState = new EnemyDeadState(this);
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

    private void Start()
    {
        if (player == null)
        {
            GameObject found =
                GameObject.FindGameObjectWithTag("Player");

            if (found != null)
                player = found.transform;
        }

        if (player == null)
        {
            Debug.LogError("Player bulunamadı!");
            enabled = false;
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("Enemy NavMesh üzerinde değil!");
            enabled = false;
            return;
        }

        stateMachine.ChangeState(PatrolState);
    }

    private void Update()
    {
        if (CanSeePlayer)
            LastKnownPosition = player.position;

        stateMachine.Update();
    }

    private void OnDied()
    {
        stateMachine.ChangeState(DeadState);
    }

    public void ChangeState(IEnemyState newState)
    {
        stateMachine.ChangeState(newState);
    }

    public void MoveTo(Vector3 destination)
    {
        if (!agent.enabled || !agent.isOnNavMesh)
            return;

        agent.isStopped = false;
        agent.SetDestination(destination);
    }

    public void StopMoving()
    {
        if (!agent.enabled || !agent.isOnNavMesh)
            return;

        agent.isStopped = true;
        agent.ResetPath();
    }

    public void DamagePlayer()
    {
        if (!CanSeePlayer || !InAttackRange)
            return;

        Health playerHealth =
            player.GetComponent<Health>();

        if (playerHealth != null)
            playerHealth.TakeDamage(attackDamage);
    }

    public bool HasReachedDestination()
    {
        if (!agent.isOnNavMesh || agent.pathPending)
            return false;

        return agent.remainingDistance <=
               agent.stoppingDistance &&
               (!agent.hasPath ||
                agent.velocity.sqrMagnitude < 0.01f);
    }

    public void FleeFromPlayer()
    {
        if (player == null)
            return;

        Vector3 direction =
            transform.position - player.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            direction = -transform.forward;

        Vector3 desiredPosition =
            transform.position +
            direction.normalized * 6f;

        if (NavMesh.SamplePosition(
            desiredPosition,
            out NavMeshHit hit,
            8f,
            NavMesh.AllAreas))
        {
            MoveTo(hit.position);
        }
    }
}