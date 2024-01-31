using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContactEnemyController : EnemyController
{
    [SerializeField][Range(0f, 1000f)] private float followRange; //플레이어를 인지하는 범위
    [SerializeField] private string targetTag = "Player";//공격할 타겟의 태그
    [SerializeField] private bool isTargetPlayer = true;
    private bool _isCollidingWithTarget; //플레이어와 접촉했는지 bool값

    [SerializeField] private SpriteRenderer characterRenderer;
    private HealthSystem healthSystem;
    private HealthSystem _collidingTargetHealthSystem;
    private PlayerMovement _collidingMovement;
    protected override void Start()
    {
        base.Start();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamage += OnDamage; //자신이 데미지를 받았을 때 처리 함수 구독
    }

    private void OnDamage() // 인지 범위를 1000f로 한다.
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
        CallMoveEvent(direction); //이동 실행
        Rotate(direction);//스프라이트 전환 실행
    }

    private void Rotate(Vector2 direction) // 스프라이트 전환
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
        if(_collidingTargetHealthSystem != null) //부딪친 상대의 헬스시스템이 있다면
        {
            _isCollidingWithTarget = true; //접촉한 것으로 한다.
        }
        _collidingMovement = receiver.GetComponent<PlayerMovement>();// 상대의 이동 스크립트를 참조한다.
    }
    private void OnTriggerExit2D(Collider2D collision) //충돌 후 떨어질 때
    {
        GameObject receiver = collision.gameObject;
        if (!receiver.CompareTag(targetTag)) // 플레이어와 부딪치지 않았다면
        {
            return;
        }
        _isCollidingWithTarget = false; // 접촉하지 않은 것으로 한다.
    }
    private void ApplyHealthChange()
    {
        AttackSO attackSO = Stats.CurrentStats.attackSO; //현재의 공격 정보
        bool hasBeenChanged = _collidingTargetHealthSystem.ChangeHealth(-attackSO.power); //상대의 헬스시스템에게 자신의 공격력만큼 데미지를 가한다.
        if (attackSO.isOnKnockback && _collidingMovement != null)// 자신의 공격 정보에 넉백이 있고 상대의 이동 스크립트가 있다면
        {
            _collidingMovement.ApplyKnockback(transform,attackSO.knockbackPower,attackSO.knockbackTime); //상대를 넉백 시킨다.
        }
        if (isTargetPlayer == true) //타겟이 플레이어라면
        {
            GameManager.instance.ChangeHpBar(attackSO.power);//플레이어의 체력바를 감소시킨다.
        }
    }
}
