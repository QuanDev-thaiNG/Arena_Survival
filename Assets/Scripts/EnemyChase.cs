using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChase : EnemyBase
{
    [Header("Chase Settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _attackRange    = 1.5f;
    [SerializeField] private float _attackCooldown = 1f;

    private NavMeshAgent _agent;
    private float _attackTimer;

    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = moveSpeed;

    }

    private void Update()
    {
        if (_target == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _target.position);
        if (distanceToPlayer <= _attackRange)
        {
            Attack();
        }
        else
        {
            Chase();
        }

        _attackTimer -= Time.deltaTime;
    }

    private void Chase()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_target.position);
    }

    private void Idle()
    {
        _agent.isStopped = true;
    }

    private void Attack()
    {
        _agent.isStopped = true;

        Vector3 dir = (_target.position - transform.position).normalized;
        dir.y = 0f;
        transform.rotation = Quaternion.LookRotation(dir);

        if (_attackTimer <= 0f)
        {
            _target.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            _attackTimer = _attackCooldown;
            Debug.Log($"{name} attacked player for {damage} damage!");
        }
    }

    protected override void Die()
    {
        _agent.isStopped = true;
        base.Die();
    }

    private void OnDrawGizmosSelected()
    {
        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}