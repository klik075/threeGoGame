using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player1,
    Player2,
    Enemy1,
    Enemy2
}

public class CharacterStat
{
    public CharacterType type;
    public float maxHp;
    public float speed;
    public StatSO statInfo;
}
