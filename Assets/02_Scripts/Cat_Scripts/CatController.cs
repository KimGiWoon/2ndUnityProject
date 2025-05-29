using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    CatMovement _catMovement;
    CatStatus _catStatus;
    

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_catStatus._isAlive)
        {
            CatMovement();
            _catMovement.GetJump();
        }
    }

    private void Init()
    {
        _catMovement = GetComponent<CatMovement>();
        _catStatus = GetComponent<CatStatus>();
        _catStatus._isAlive = true;
    }

    private void CatMovement()
    {
        // Cat Move
        Vector3 moveDir = _catMovement.SetMove();
        bool isRun = moveDir != Vector3.zero ? true : false;
        _catMovement._catAnimator.SetBool("Run", isRun);

        _catMovement.SetRotation(moveDir);
        _catMovement.SetMouseRotation();
    }

}
