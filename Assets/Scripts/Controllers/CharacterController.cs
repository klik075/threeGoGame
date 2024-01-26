using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    private float _timeSinceLastAtteck = float.MaxValue;
    protected bool IsAttacking { get; set; }

    protected CharacterStatHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStatHandler>();
    }
    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        //{
        //    if (_timeSinceLastAtteck <= 0.2f)
        //    {
        //        _timeSinceLastAtteck += Time.deltaTime;
        //    }
        //    else if (IsAttacking)
        //    {
        //        _timeSinceLastAtteck = 0;
        //        CallAttackEvent();
        //    }
        //}
        {
            if (Stats.CurrentStats.attackSO == null)
                return; 

            if (_timeSinceLastAtteck <= Stats.CurrentStats.attackSO.delay) 
            {
                _timeSinceLastAtteck += Time.deltaTime;
            }

            if (IsAttacking && _timeSinceLastAtteck > Stats.CurrentStats.attackSO.delay)
            {
                _timeSinceLastAtteck = 0;
                CallAttackEvent(Stats.CurrentStats.attackSO); 
               
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
