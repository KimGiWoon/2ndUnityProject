using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour, IDamagable
{
    [SerializeField] Animator _catAnimator;
    [SerializeField] float _rollStartVel = -5f;
    [SerializeField] float recordTime = 3f;

    public bool _isActiveControl { get; set; } = true;

    Vector3 _moveVec;
    Vector3 _currentPosition;
    public Vector3 _firstPosition;
    public Vector3 _secondPosition;
    CatMovement _catMovement;
    CatStatus _catStatus;
    Rigidbody _catRigidbody;

    bool _isGround;
    bool _isJumpTrap = false;
    bool _isRoll;
    bool _isRewinding = false;
    float _DownTime = 0;
    float _time;

    WaitForSeconds _positionSaveTime;
    List<Vector3> positionHistory = new List<Vector3>();
    List<Quaternion> rotationHistory = new List<Quaternion>();


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
            FirstPositionTel();
        }
        else
        {
            CatDie();
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
        _firstPosition = transform.position;
        _catStatus._curHp = _catStatus._maxHp;
        _catRigidbody = GetComponent<Rigidbody>();
        _time = Time.deltaTime;
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
        if (_isRewinding)
        {
            RewindStep();
        }
        else
        {
            RecordStep();
        }

        if (_catMovement._catRigid.velocity.y < _rollStartVel)
        {
            _catAnimator.SetBool("Spin", true);
            _isRoll = true;

            _DownTime += Time.fixedDeltaTime;

            if (_DownTime > 0.5f && !_isJumpTrap)
            {
                // 되감기 시작
                StartRewind();

                //PositionReset();
            }
        }
    }
    // TODO : 임시 주석처리 (추후 삭제 예정)
    private void PositionReset()
    {
        gameObject.transform.position = _currentPosition;
        _DownTime = 0;
    }

    public void GetJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            // StopCoroutine(_posCoroutine);
            _catMovement._catRigid.AddForce(Vector3.up * _catStatus._jumpPower, ForceMode.Impulse);
            _catAnimator.SetBool("Jump", true);
            _isGround = false;

            RecordStep();
            DownSpin();
        }
    }
    // 
    private void RewindStep()
    {
        if (positionHistory.Count > 0)
        {
            transform.position = positionHistory[0];
            transform.rotation = rotationHistory[0];
            positionHistory.RemoveAt(0);
            rotationHistory.RemoveAt(0);
        }
        else
        {
            StopRewind(); // 기록이 다 떨어지면 종료
        }
    }

    private void RecordStep()
    {
        // 위치와 회전 저장
        positionHistory.Insert(0, transform.position);
        rotationHistory.Insert(0, transform.rotation);

        // 기록 시간 제한
        int maxSteps = Mathf.RoundToInt(recordTime / _time);
        if (positionHistory.Count > maxSteps)
        {
            positionHistory.RemoveAt(positionHistory.Count - 1);
            rotationHistory.RemoveAt(rotationHistory.Count - 1);
        }
    }

    private void StartRewind()
    {
        _isRewinding = true;
        _catRigidbody.isKinematic = true; // 물리 작용 끄기
    }

    private void StopRewind()
    {
        _isRewinding = false;
        _catRigidbody.isKinematic = false; // 물리 다시 켜기
    }

    private void CatDie()
    {
        transform.position = _secondPosition;
    }

    private void FirstPositionTel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = _firstPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _catAnimator.SetBool("Jump", false);
            _catAnimator.SetBool("Spin", false);
            _catAnimator.SetBool("Swim", false);
            _isGround = true;
            _isRoll = false;
            _isJumpTrap = false;
            //_posCoroutine = StartCoroutine(Positionlutin());

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _catAnimator.SetBool("Swim", true);
            _catAnimator.SetBool("Spin", false);
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

    //public IEnumerator Positionlutin()
    //{
    //    _currentPosition = transform.position;
    //    yield return _positionSaveTime;
    //}

    public void TakeDamage(int damage)
    {
        _catStatus._curHp -= damage;
        
        if(_catStatus._curHp == 0)
        {
            _catAnimator.SetBool("Death", true);
            _catStatus._isAlive = false;
        }
    }
}
