using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour, IDamagable
{
    [SerializeField] Animator _catAnimator;
    [SerializeField] float _rollStartVel = -5f;
    [SerializeField] float _posSaveTime = 1f;

    public bool _isActiveControl { get; set; } = true;
    Vector3 _moveVec;
    Vector3 _currentPosition;
    CatMovement _catMovement;
    CatStatus _catStatus;

    bool _isGround;
    bool _isJumpTrap = false;
    bool _isRoll;
    float _DownTime = 0;
    
    public Coroutine _posCoroutine;
    WaitForSeconds _positionSaveTime;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_catStatus._isAlive)
        {
            if (!_isActiveControl) return;
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
    }

    private void CatMovement()
    {
        // Cat Move
        _moveVec = _catMovement.SetMove();
        //bool isRun = moveDir != Vector3.zero ? true : false;
        _catAnimator.SetFloat("MoveSpeed", _moveVec.magnitude);

        _catMovement.SetRotation(_moveVec);
        _catMovement.SetMouseRotation();
    }

    private void DownSpin()
    {

        if (_catMovement._catRigid.velocity.y < _rollStartVel)
        {
            _catAnimator.SetBool("Roll", true);
            _isRoll = true;

            _DownTime += Time.fixedDeltaTime;

            if (_DownTime > 0.5f && !_isJumpTrap)
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
            _isRoll = false;
            _isJumpTrap = false;
            _posCoroutine = StartCoroutine(Positionlutinroutine());

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _catAnimator.SetBool("Swim", true);
            _catAnimator.SetBool("Roll", false);
            _isGround = false;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            _isJumpTrap = true;
            if (!_isRoll)
            {
                _catAnimator.SetTrigger("Bounce");
            }
        }

    }

    public IEnumerator Positionlutinroutine()
    {
        _currentPosition = gameObject.transform.position;
        yield return _positionSaveTime;
    }

    public void TakeDamage(int damage)
    {
        
    }
}
