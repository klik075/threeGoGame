using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player1,
    Player2
}

[Serializable]
public class CharacterStat
{
    public CharacterType type;
    public float maxHp;
    public float speed;
    public CharacterStatSO statInfo;
    ////////////////////////////////
    public string characterName; //위 아래 고려
    public string info;
    public int lv;
    public float exp;
    public float fullExp;
    public AttackSO attackSO;
}
