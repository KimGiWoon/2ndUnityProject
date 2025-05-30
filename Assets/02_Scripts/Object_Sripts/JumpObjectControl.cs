using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObjectControl : MonoBehaviour
{
    [SerializeField] float pushPower = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Rigidbody collisionRigid = collision.rigidbody;

            if (collisionRigid != null && !collisionRigid.isKinematic)
            {
                collisionRigid.AddForce(Vector3.up * pushPower, ForceMode.Impulse);
            }
        }
    }
    
}
