using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEnemyData", menuName ="Enemy/StatInfo/Default", order = 0)]
public class EnemyStatSO : ScriptableObject
{
    [Header("Enemy Info")]
    public string name;
    public string info;
    public int lv;
    public float hp;
    public float attack;
    public float size;
    public float speed;

    [Header("Get Hit Info")]
    public bool isHit;
    public float knockbackPower;
    public float knockbackTime;
}
