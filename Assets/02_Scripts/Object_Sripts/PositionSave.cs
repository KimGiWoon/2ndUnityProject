using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSave : MonoBehaviour
{
    [SerializeField] CatController _catCon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _catCon._secondPosition = other.transform.position;
        }
    }
}
