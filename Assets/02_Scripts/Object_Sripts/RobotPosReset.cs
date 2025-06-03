using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotPosReset : MonoBehaviour
{
    [SerializeField] EnemyController[] _robotCon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            for (int i  = 0; i < _robotCon.Length; i++)
            {
                _robotCon[i].transform.position = _robotCon[i]._firstPosition;
                _robotCon[i]._canTrace = false;
                _robotCon[i].gameObject.SetActive(false);
            }
        }
    }
}
