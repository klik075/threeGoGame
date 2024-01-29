using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContactEnemyController : EnemyController
{
    [SerializeField][Range(0f, 1000f)] private float followRange;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private bool isTargetPlayer = true;
    private bool _isCollidingWithTarget;

    private AttackSO _attackData;
    [SerializeField] private SpriteRenderer characterRenderer;
    private HealthSystem healthSystem;
    private HealthSystem _collidingTargetHealthSystem;
    private PlayerMovement _collidingMovement;
    protected override void Start()
    {
        base.Start();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamage += OnDamage; //자신이 데미지를 받았을 때 처리 함수
        _attackData = GetComponent<CharacterStatHandler>().CurrentStats.attackSO;
    }

    private void OnDamage()
    {
        followRange = 1000f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_isCollidingWithTarget) //충돌 종료 후 실행
        {
            ApplyHealthChange();
        }

        Vector2 direction = Vector2.zero;
        if (DistanceToTarget() < followRange) //사정거리 안에 들어오면
        {
            direction = DirectionToTarget(); //방향을 플레이쪽으로
        }
        CallMoveEvent(direction);
        Rotate(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg; // 각도 설정
        characterRenderer.flipX = Math.Abs(rotZ) > 90f; //왼쪽을 바라보면 스프라이트도 왼쪽으로
    }
    private void OnTriggerEnter2D(Collider2D collision) // 충돌 시작
    {
        GameObject receiver = collision.gameObject;
        if (!receiver.CompareTag(targetTag)) // 플레이어와 부딪치지 않았다면
        {
            return;
        }
        _collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>(); 
        if(_collidingTargetHealthSystem != null)
        {
            _isCollidingWithTarget = true; //접촉한 것으로 본다.
        }
        _collidingMovement = receiver.GetComponent<PlayerMovement>();
    }
    private void OnTriggerExit2D(Collider2D collision) //충돌 후 떨어질 때
    {
        GameObject receiver = collision.gameObject;
        if (!receiver.CompareTag(targetTag)) // 플레이어와 부딪치지 않았다면
        {
            return;
        }
        _isCollidingWithTarget = false;
    }
    private void ApplyHealthChange()
    {
        AttackSO attackSO = Stats.CurrentStats.attackSO;
        bool hasBeenChanged = _collidingTargetHealthSystem.ChangeHealth(-attackSO.power);
        if (attackSO.isOnKnockback && _collidingMovement != null)
        {
            _collidingMovement.ApplyKnockback(transform,attackSO.knockbackPower,attackSO.knockbackTime);
        }
        if (isTargetPlayer == true)
        {
            GameManager.instance.ChangeHpBar(_attackData.power);
        }
    }
}
