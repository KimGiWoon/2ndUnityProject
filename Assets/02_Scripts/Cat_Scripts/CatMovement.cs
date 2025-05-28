using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _avatar;

    Rigidbody _catRigid;
    CatStatus _catStatus;
    Vector3 _moveVec;
    Vector2 _rotationVec;

    [Header("Mouse Config")]
    [SerializeField][Range(-90, 0)] float _minPitch;
    [SerializeField][Range(0, 90)] float _maxPitch;
    [SerializeField][Range(0, 5)] float _mouseSensitivity = 1;

    private void Awake()
    {
        Init();
    }

    private void Init() // Compoenent Initial
    {
        _catRigid = GetComponent<Rigidbody>();
        _catStatus = GetComponent<CatStatus>();
    }

    public void GetMoveInput()  // Move key Input
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        _moveVec = new Vector3(moveX, 0, moveZ);
    }

    public void GetMouseInput() // Mouse Button Input
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationVec =  new Vector2(mouseX, mouseY);
    }

    public void SetMove()   // Cat Movement
    {
        GetMoveInput();
        _catRigid.velocity = Vector3.MoveTowards(_catRigid.velocity, _moveVec * _catStatus._moveSpeed, _catStatus._acceleSpeed);
    }

    public void SetRotation()   // Cat Move Rotation 
    {
        if (_catRigid.velocity == Vector3.zero)
        {
            return;
        }
        Quaternion catRotation = Quaternion.LookRotation(_catRigid.velocity);
        _avatar.rotation = Quaternion.Lerp(_avatar.rotation, catRotation, _catStatus._rotateSpeed * Time.deltaTime);
    }

    public void SetMouseRotation()
    {
        

    }

}
