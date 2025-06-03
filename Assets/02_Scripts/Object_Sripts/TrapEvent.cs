using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onTrigger;
    [SerializeField] EnemyController[] _enemyControl;
    [SerializeField] bool _Retry;
    bool _firstIn = true;

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            for(int i  = 0; i < _enemyControl.Length; i++)
            {
                _enemyControl[i]._canTrace = true;
                _enemyControl[i].gameObject.SetActive(true);
            }
            if (_firstIn)
            {
                _onTrigger.Invoke();
                _firstIn = false;
            }
            
            if (!_Retry)
            {
                Destroy(gameObject);
            }
        }
    }
}
