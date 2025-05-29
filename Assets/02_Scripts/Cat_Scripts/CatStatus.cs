using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatus : MonoBehaviour
{
    [Header("Cat Status")]
    
    // Cat move Speed
    [SerializeField] float moveSpeed;
    public float _moveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    // Cat Jump Power
    [SerializeField] float jumpPower;
    public float _jumpPower { get { return jumpPower; } set { jumpPower = value; } }

    // Cat Rotate Speed
    [SerializeField] float rotateSpeed;
    public float _rotateSpeed { get {return rotateSpeed; } set { rotateSpeed = value; } }

    // Cat Alive Check
    [SerializeField] bool isAlive;
    public bool _isAlive { get { return isAlive; } set { isAlive = value; } }


}
