using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] Animator _catAnimator;
    [SerializeField] float _rollStartVel = -5f;
    [SerializeField] float _posSaveTime = 1f;
    Vector3 _currentPosition;
    CatMovement _catMovement;
    CatStatus _catStatus;
    bool _isGround;
    public Coroutine _posCoroutine;
    WaitForSeconds _positionSaveTime;
    float _DownTime;

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
        _positionSaveTime = new WaitForSeconds(_posSaveTime);
        _DownTime = 0;
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

        if (_catMovement._catRigid.velocity.y < _rollStartVel)
        {
            _catAnimator.SetBool("Roll", true);

            _DownTime += Time.fixedDeltaTime;
            Debug.Log($"다운 타임 : {_DownTime}");
            if (_DownTime > 0.5f)
            {
                PositionReset();
            }
        }
    }

    private void PositionReset()
    {
        gameObject.transform.position = _currentPosition;
        _DownTime = 0;
    }

    public void GetJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            StopCoroutine(_posCoroutine);
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
            _posCoroutine = StartCoroutine(Positionlutinroutine());

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _catAnimator.SetBool("Swim", true);
            _catAnimator.SetBool("Roll", false);
            _isGround = false;
            Debug.Log("물에 들어옴");
        }

    }

    public IEnumerator Positionlutinroutine()
    {
        _currentPosition = gameObject.transform.position;
        Debug.Log(_currentPosition);
        yield return _positionSaveTime;
    }
}
