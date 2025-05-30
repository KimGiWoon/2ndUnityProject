using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _avatar;
    [SerializeField] Transform _cam;

    public Rigidbody _catRigid;
    CatStatus _catStatus;
    Vector2 _rotationVec;

    [Header("Mouse Config")]
    [SerializeField][Range(-90, 0)] float _minRange;
    [SerializeField][Range(0, 90)] float _maxRange;
    [SerializeField][Range(0, 5)] float _mouseSensitivity = 1;

    private void Awake()
    {
        Init();
    }

    private void Init() // Compoenent Initial
    {
        _catStatus = GetComponent<CatStatus>();
        _catRigid = GetComponent<Rigidbody>();
    }

    public Vector3 GetMoveInput()  // Move key Input
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(moveX, 0, moveZ).normalized;

        Vector3 camForward = _cam.forward;
        Vector3 camRight = _cam.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;

        return moveDir.normalized;
    }

    public Vector2 GetMouseInput() // Mouse Button Input
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;

        return new Vector2(mouseX, mouseY);
    }
    
    public Vector3 SetMove()   // Cat Movement
    {
        Vector3 moveDir =  GetMoveInput();
        Vector3 moveVelocity = _catRigid.velocity;
        moveVelocity.x = moveDir.x * _catStatus._moveSpeed;
        moveVelocity.z = moveDir.z * _catStatus._moveSpeed;
        _catRigid.velocity = moveVelocity;

        return moveDir;
    }

    public void SetRotation(Vector3 moveDir)   // Cat Move Rotation 
    {
        if (moveDir == Vector3.zero)
        {
            return;
        }
        Quaternion catRotation = Quaternion.LookRotation(moveDir);
        _avatar.rotation = Quaternion.Slerp(_avatar.rotation, catRotation, _catStatus._rotateSpeed * Time.deltaTime);
    }

    public void SetMouseRotation()  // Camera Rotation
    {
        Vector2 mouseDir = GetMouseInput();

        _rotationVec.x += mouseDir.x;
        _rotationVec.y = Mathf.Clamp(_rotationVec.y + mouseDir.y, _minRange, _maxRange);
        transform.rotation = Quaternion.Euler(0, _rotationVec.x, 0);

        _cam.localEulerAngles = new Vector3(_rotationVec.y, 0, 0);

    }

    
}
