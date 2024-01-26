using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player1,
    Player2,
    Monster1,
    Monster2
}

public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}



[Serializable]
public class CharacterStat
{
    public CharacterType Charactertype;
    public StatsChangeType statsChangeType;
    public string name;
    public string info;
    public int lv;
    public float exp;
    public float fullExp;
    public float hp;
    public float attack;
    [Range(1, 100)] public float maxHp;
    [Range(1f, 20f)] public float speed;
    public AttackSO attackSO;

    
    

   
}
