using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public enum CharacterType
    {
        PlayerCat,
        PlayerDog,
        Monster1,
        Monster2
    }

    public string characterName;
    public string characterInfo;
    public int lv;
    public float exp;
    public float fullExp;
    public float hp;
    public float attack;
}
