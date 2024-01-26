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
    public CharacterType type;
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public string characterName; //?? ???? ????
    public string info;
    public int lv;
    public float exp;
    public float fullExp;
    public AttackSO attackSO;
}
