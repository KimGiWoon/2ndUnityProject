using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] CatStatus _catStatus;
    Rigidbody _catRigid;
    Animator _catAnimator;
    Vector3 _moveVec;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_catStatus._isAlive)
        {
            SetMove();

        }
    }

    private void FixedUpdate()
    {

    }

    private void Init()
    {
        _catRigid = GetComponent<Rigidbody>();
        _catAnimator = GetComponent<Animator>();
        _catStatus._isAlive = true;
    }

    private void SetMove()
    {
        // Move Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        _moveVec = new Vector3(moveX, 0 , moveZ);

        _catRigid.velocity = Vector3.MoveTowards(_catRigid.velocity, _moveVec * _catStatus._moveSpeed, _catStatus._acceleSpeed);
    }

    private void SetRotation()
    {

    }

}
