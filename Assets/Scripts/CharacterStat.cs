using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player1,
    Player2
}

public enum StatsChangeType
{
    Add,
    Multiple,
    Override,
}

[Serializable]
public class CharacterStat
{
    public CharacterType type;
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public CharacterStatSO statInfo;
    ////////////////////////////////
    public string characterName; //?? ???? ????
    public string info;
    public int lv;
    public float exp;
    public float fullExp;
    public AttackSO attackSO;

    public StatsChangeType statsChangeType;
    

   
}
