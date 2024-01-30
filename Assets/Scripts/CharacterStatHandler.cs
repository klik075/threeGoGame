using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStats;
    public CharacterStat CurrentStats { get; private set; }

    public List<CharacterStat> statsModifiers = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStats();
    }
    // Start is called before the first frame update
    
    private void UpdateCharacterStats() //수정 고려
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
           attackSO = Instantiate(baseStats.attackSO);
        }
        CurrentStats = new CharacterStat { attackSO = attackSO };
        CurrentStats.characterName = this.gameObject.name;
        CurrentStats.maxHealth = baseStats.maxHealth;
        CurrentStats.speed = baseStats.speed;
        CurrentStats.characterName = baseStats.characterName;
        CurrentStats.exp = baseStats.exp;
        CurrentStats.lv = baseStats.lv;
        CurrentStats.info = baseStats.info;
        CurrentStats.fullExp = baseStats.fullExp;
        //추가작업
    }
    
}
