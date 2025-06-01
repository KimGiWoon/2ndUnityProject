using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onTrigger;
    [SerializeField] EnemyController _enemyControl;
    [SerializeField] bool _Retry;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _enemyControl._canTrace = true;
            _onTrigger.Invoke();

            if (!_Retry)
            {
                Destroy(gameObject);
            }
        }
    }
}
