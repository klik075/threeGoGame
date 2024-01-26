using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat CharacterbaseStats;
    public CharacterStat CurrentStats { get; private set; }

    public List<CharacterStat> CstatsModifier = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStats();
    }


    private void UpdateCharacterStats() //수정 고려
    {
        AttackSO attackSO = null;
        if (CharacterbaseStats.attackSO != null)
        {
            attackSO = Instantiate(CharacterbaseStats.attackSO);
        }
        CurrentStats = new CharacterStat { attackSO = attackSO };
        CurrentStats.maxHp = CharacterbaseStats.maxHp;
        CurrentStats.speed = CharacterbaseStats.speed;
        //������ ���ݵ鵵 �����ؾ� ��
    }

    //private void UpdateCharacterStats()
    //{
    //    CharacterStatSO CharacterSO = null;
    //    if (CharacterbaseStats.statInfo != null)
    //    {
    //        CharacterSO = Instantiate(CharacterbaseStats.statInfo);
    //    }

    //    CurrentStats = new CharacterStat { statInfo = CharacterSO };
    //}
}
