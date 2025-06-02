using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatus : MonoBehaviour
{
    [Header("Cat Status")]

    // Cat Max hp
    [SerializeField] int maxHp;
    public int _maxHp { get { return maxHp; } set { maxHp = value; } }

    // Cat Current Hp
    [SerializeField] int curHp;
    public int _curHp { get { return curHp; } set { curHp = value; } }
    
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
