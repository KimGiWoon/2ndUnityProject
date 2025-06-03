using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosResetControl : MonoBehaviour
{
    [SerializeField] CatController _catCon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.position = _catCon._firstPosition;
        }
    }
}
