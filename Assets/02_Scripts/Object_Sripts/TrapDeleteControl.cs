using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRemove : MonoBehaviour
{
    [SerializeField] GameObject _posResetTrap1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _posResetTrap1.SetActive(false);
        }
    }
}
