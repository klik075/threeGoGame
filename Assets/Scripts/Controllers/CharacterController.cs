using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    private float _timeSinceLastAtteck = float.MaxValue;//공격 딜레이 시간 체크
    protected bool IsAttacking { get; set; } //공격 가능 여부

    protected CharacterStatHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStatHandler>();
    }
    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()//공격이 가능하면 공격 실행
    {
        {
            if (Stats.CurrentStats.attackSO == null) //자신의 공격 정보가 없으면
                return; 

            if (_timeSinceLastAtteck <= Stats.CurrentStats.attackSO.delay) //공격 딜레이보다 시간이 작다면
            {
                _timeSinceLastAtteck += Time.deltaTime;//시간 증가
            }

            if (IsAttacking && _timeSinceLastAtteck > Stats.CurrentStats.attackSO.delay)//공격 가능 상태이고 딜레이의 시간을 넘겼다면
            {
                _timeSinceLastAtteck = 0;//시간 초기화
                CallAttackEvent(Stats.CurrentStats.attackSO);//공격 실행
            }
        }
    }
    protected virtual void FixedUpdate()
    {
        
    }
    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallAttackEvent(AttackSO attackSO) 
    {
        OnAttackEvent?.Invoke(attackSO);
    }
}
