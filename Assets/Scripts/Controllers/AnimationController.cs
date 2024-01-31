using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.PluginConfigurationItemMetadata;

public class AnimationController : Animations
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    private HealthSystem _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        if (gameObject.tag == "Player")// 자신의 태그가 플레이어라면
        {
            controller.OnAttackEvent += Attacking;//공격 애니메이션 구독
        }
        controller.OnMoveEvent += Move;//이동 애니메이션 구독

        if (_healthSystem != null)
        {
            _healthSystem.OnDamage += Hit;//피격 애니메이션 구독
            _healthSystem.OnInvincibilityEnd += InvincibilityEnd;//피격 후 애니메이션 구독
        }
    }
    
    private void Attacking(AttackSO obj)
    {
        animator.SetTrigger(Attack);
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > .5f);
    }

    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }
    private void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}
