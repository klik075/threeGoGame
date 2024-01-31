using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    private float _timeSinceLastAtteck = float.MaxValue;//���� ������ �ð� üũ
    protected bool IsAttacking { get; set; } //���� ���� ����

    protected CharacterStatHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStatHandler>();
    }
    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()//������ �����ϸ� ���� ����
    {
        {
            if (Stats.CurrentStats.attackSO == null) //�ڽ��� ���� ������ ������
                return; 

            if (_timeSinceLastAtteck <= Stats.CurrentStats.attackSO.delay) //���� �����̺��� �ð��� �۴ٸ�
            {
                _timeSinceLastAtteck += Time.deltaTime;//�ð� ����
            }

            if (IsAttacking && _timeSinceLastAtteck > Stats.CurrentStats.attackSO.delay)//���� ���� �����̰� �������� �ð��� �Ѱ�ٸ�
            {
                _timeSinceLastAtteck = 0;//�ð� �ʱ�ȭ
                CallAttackEvent(Stats.CurrentStats.attackSO);//���� ����
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
