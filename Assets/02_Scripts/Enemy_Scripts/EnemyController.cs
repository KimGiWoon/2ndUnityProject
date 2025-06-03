using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] public Animator _enemyAnimator;

    NavMeshAgent _navMeshAgent;
    EnemtStatus _status;
    public Vector3 _firstPosition;
    public bool _canTrace = false;
    public bool _isActiveContol { get; set; } = false;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_status._isAlive)
        {
            if (!_isActiveContol) return;
            EnemyMove();
        }
        else
        {
            EnemyDie();
        }
    }

    private void Init()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemtStatus>();
        _status._isAlive = true;
        _firstPosition = transform.position;
    }

    private void EnemyMove()
    {
        if (_target == null)
        {
            return;
        }
        if (_canTrace)
        {
            _navMeshAgent.SetDestination(_target.position);
            _enemyAnimator.SetBool("Run", true);
        }
        else
        {
            _enemyAnimator.SetBool("Run", false);
        }
    }

    private void EnemyDie()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CatController _cat = collision.gameObject.GetComponent<CatController>();

            if (_cat != null)
            {
                _cat.TakeDamage(_status._enemyDamage);
            }

        }
    }

    
}
