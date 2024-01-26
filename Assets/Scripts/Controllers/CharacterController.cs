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

        if (Stats.CurrentStats.attackSO == null)
            return; // attackso 가 없으면 공격 안함 

        if (_timeSinceLastAtteck <= Stats.CurrentStats.attackSO.delay) // attackso 에 잡아놓은 값으로 딜레이 처리 
        {
            _timeSinceLastAtteck += Time.deltaTime;
        }

        if (IsAttacking && _timeSinceLastAtteck > Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAtteck = 0;
            CallAttackEvent(Stats.CurrentStats.attackSO); // Input 에서 event를 call 하면 구독되어 있는것에 신호를 준다
            // 근데 이것도 컨트롤러임 슈팅은 어택이벤트에서 한다 
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

    public void CallAttackEvent(AttackSO attackSO) //AttackSO attackSO
    {
        OnAttackEvent?.Invoke(attackSO);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
