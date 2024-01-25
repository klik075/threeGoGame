using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Monster1,
    Monster2
}

public class EnemyStat
{
    public EnemyType type;
    public float maxHp;
    [Range(1, 100)] public float speed;
    public EnemyStatSO statInfo;
}
