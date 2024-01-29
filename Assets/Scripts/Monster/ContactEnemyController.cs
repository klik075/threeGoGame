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
        healthSystem.OnDamage += OnDamage; //�ڽ��� �������� �޾��� �� ó�� �Լ�
        _attackData = GetComponent<CharacterStatHandler>().CurrentStats.attackSO;
    }

    private void OnDamage()
    {
        followRange = 1000f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_isCollidingWithTarget) //�浹 ���� �� ����
        {
            ApplyHealthChange();
        }

        Vector2 direction = Vector2.zero;
        if (DistanceToTarget() < followRange) //�����Ÿ� �ȿ� ������
        {
            direction = DirectionToTarget(); //������ �÷���������
        }
        CallMoveEvent(direction);
        Rotate(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg; // ���� ����
        characterRenderer.flipX = Math.Abs(rotZ) > 90f; //������ �ٶ󺸸� ��������Ʈ�� ��������
    }
    private void OnTriggerEnter2D(Collider2D collision) // �浹 ����
    {
        GameObject receiver = collision.gameObject;
        if (!receiver.CompareTag(targetTag)) // �÷��̾�� �ε�ġ�� �ʾҴٸ�
        {
            return;
        }
        _collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>(); 
        if(_collidingTargetHealthSystem != null)
        {
            _isCollidingWithTarget = true; //������ ������ ����.
        }
        _collidingMovement = receiver.GetComponent<PlayerMovement>();
    }
    private void OnTriggerExit2D(Collider2D collision) //�浹 �� ������ ��
    {
        GameObject receiver = collision.gameObject;
        if (!receiver.CompareTag(targetTag)) // �÷��̾�� �ε�ġ�� �ʾҴٸ�
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
