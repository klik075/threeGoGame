using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultCharacterData", menuName = "Character/StatInfo/Default", order = 1)]
public class CharacterStatSO : ScriptableObject
{
    [Header("Character Info")]
    public string name;
    public string info;
    public int lv;
    public float exp;
    public float fullExp;
    public float hp;
    public float attack;
    public float size;
    public float speed;

    [Header("Get Hit Info")]
    public bool isHit;
    public float knockbackPower;
    public float knockbackTime;
}
