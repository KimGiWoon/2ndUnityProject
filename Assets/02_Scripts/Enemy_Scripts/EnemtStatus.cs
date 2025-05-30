using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemtStatus : MonoBehaviour
{
    [Header("Enemy Status")]

    // Enemy Hp
    [SerializeField] int enemyHp;
    public int _enemyHp { get { return _enemyHp; } set { _enemyHp = value; } }

    // Enemy Damage
    [SerializeField] int enemyDamage;
    public int _enemyDamage { get { return _enemyDamage; } set { _enemyDamage = value; } }

    // Enemy Alive Check
    [SerializeField] bool isAlive;
    public bool _isAlive { get { return isAlive; } set { isAlive = value; } }
}
