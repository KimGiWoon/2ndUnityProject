using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] Transform _target;

    NavMeshAgent _navMeshAgent;
    EnemtStatus _status;
    bool _canTrace = true;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_status._isAlive)
        {
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
        //_navMeshAgent.isStopped = true;
        _status = GetComponent<EnemtStatus>();
        _status._isAlive = true;
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
        }

        //_navMeshAgent.isStopped = !_canTrace;
    }

    private void EnemyDie()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        CatController _cat = collision.gameObject.GetComponent<CatController>();

    //        if (_cat != null)
    //        {
    //            _cat.TakeDamage(_status._enemyDamage);
    //        }
           
    //    }
    //}
}
