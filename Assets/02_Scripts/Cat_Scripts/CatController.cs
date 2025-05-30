using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] Animator _catAnimator;
    [SerializeField] float _rollStartVel = -4f;
    CatMovement _catMovement;
    CatStatus _catStatus;
    bool _isGround;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_catStatus._isAlive)
        {
            CatMovement();
            GetJump();
        }
    }

    private void FixedUpdate()
    {
        DownSpin();
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
        //bool isRun = moveDir != Vector3.zero ? true : false;
        _catAnimator.SetFloat("MoveSpeed", moveDir.magnitude);

        _catMovement.SetRotation(moveDir);
        _catMovement.SetMouseRotation();
    }

    private void DownSpin()
    {
        if(_catMovement._catRigid.velocity.y < _rollStartVel)
        {
            _catAnimator.SetBool("Roll", true);
        }
    }

    public void GetJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _catMovement._catRigid.AddForce(Vector3.up * _catStatus._jumpPower, ForceMode.Impulse);
            _catAnimator.SetBool("Jump", true);
            _isGround = false;
            
            DownSpin();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _catAnimator.SetBool("Jump", false);
            _catAnimator.SetBool("Roll", false);
            _catAnimator.SetBool("Swim", false);
            _isGround = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _catAnimator.SetBool("Swim", true);
            _catAnimator.SetBool("Roll", false);
            _isGround = false;
            Debug.Log("¹°¿¡ µé¾î¿È");
        }

    }
   
}
