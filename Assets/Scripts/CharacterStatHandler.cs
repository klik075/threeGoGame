using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField]
    private CharacterStat CharacterbaseStats;

    public CharacterStat CurrentStats { get; private set; }

    public List<CharacterStat> CstatsModifier = new List<CharacterStat>();

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
        currentStats = new CharacterStat { attackSO = attackSO };
        currentStats.maxHp = baseStats.maxHp;
        currentStats.speed = baseStats.speed;
        //������ ���ݵ鵵 �����ؾ� ��
    }

    private void UpdateCharacterStats()
    {
        CharacterStatSO CharacterSO = null;
        if (CharacterbaseStats.statInfo != null)
        {
            CharacterSO = Instantiate(CharacterbaseStats.statInfo);
        }

        CurrentStats = new CharacterStat { statInfo = CharacterSO };
    }
}
