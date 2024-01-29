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
    private static readonly int MonsterIsDead = Animator.StringToHash("MonsterIsDead");

    private HealthSystem _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;

        if (_healthSystem != null)
        {
            _healthSystem.OnDamage += Hit;
            _healthSystem.OnInvincibilityEnd += InvincibilityEnd;
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
    public void MonsterDead()
    {
        animator.SetTrigger(MonsterIsDead);
    }
}
