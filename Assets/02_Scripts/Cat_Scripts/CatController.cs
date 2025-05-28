using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    CatMovement _catMovement;
    CatStatus _catStatus;
    Animator _catAnimator;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_catStatus._isAlive)
        {
            CatMovement();
        }
    }

    private void Init()
    {
        _catMovement = GetComponent<CatMovement>();
        _catStatus = GetComponent<CatStatus>();
        _catAnimator = GetComponent<Animator>();
        _catStatus._isAlive = true;
    }

    private void CatMovement()
    {
        _catMovement.SetMove();
        _catMovement.SetRotation();
    }

}
