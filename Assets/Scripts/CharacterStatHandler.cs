using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStats;
    public CharacterStat currentStats { get; private set; }
    public List<CharacterStat> statssModifier = new List<CharacterStat>();
    // Start is called before the first frame update
    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }
        currentStats = new CharacterStat { attackSO = attackSO };
        currentStats.maxHp = baseStats.maxHp;
        currentStats.speed = baseStats.speed;
        //나머지 스텟들도 복사해야 함
    }
}
