using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player1,
    Player2
}

public class CharacterStat
{
    public CharacterType type;
    public float maxHp;
    public float speed;
    public CharacterStatSO statInfo;
}
