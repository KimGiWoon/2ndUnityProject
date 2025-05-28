using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatus : MonoBehaviour
{
    [Header("Cat Status")]
    
    // Cat move Speed
    [SerializeField] float moveSpeed;
    public float _moveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    // Cat move Acceleration Speed
    [SerializeField] float acceleSpeed;
    public float _acceleSpeed { get { return acceleSpeed; } set { acceleSpeed = value; } }

    // Cat Jump Power
    [SerializeField] float jumpPower;
    public float _jumpPower { get { return jumpPower; } set { jumpPower = value; } }

    // Cat Alive Check
    [SerializeField] bool isAlive;
    public bool _isAlive { get { return isAlive; } set { isAlive = value; } }


}
