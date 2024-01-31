using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContactEnemyController : EnemyController
{
    [SerializeField][Range(0f, 1000f)] private float followRange; //�÷��̾ �����ϴ� ����
    [SerializeField] private string targetTag = "Player";//������ Ÿ���� �±�
    [SerializeField] private bool isTargetPlayer = true;
    private bool _isCollidingWithTarget; //�÷��̾�� �����ߴ��� bool��

    [SerializeField] private SpriteRenderer characterRenderer;
    private HealthSystem healthSystem;
    private HealthSystem _collidingTargetHealthSystem;
    private PlayerMovement _collidingMovement;
    protected override void Start()
    {
        base.Start();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamage += OnDamage; //�ڽ��� �������� �޾��� �� ó�� �Լ� ����
    }

    private void OnDamage() // ���� ������ 1000f�� �Ѵ�.
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
        CallMoveEvent(direction); //�̵� ����
        Rotate(direction);//��������Ʈ ��ȯ ����
    }

    private void Rotate(Vector2 direction) // ��������Ʈ ��ȯ
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
        if(_collidingTargetHealthSystem != null) //�ε�ģ ����� �ｺ�ý����� �ִٸ�
        {
            _isCollidingWithTarget = true; //������ ������ �Ѵ�.
        }
        _collidingMovement = receiver.GetComponent<PlayerMovement>();// ����� �̵� ��ũ��Ʈ�� �����Ѵ�.
    }
    private void OnTriggerExit2D(Collider2D collision) //�浹 �� ������ ��
    {
        GameObject receiver = collision.gameObject;
        if (!receiver.CompareTag(targetTag)) // �÷��̾�� �ε�ġ�� �ʾҴٸ�
        {
            return;
        }
        _isCollidingWithTarget = false; // �������� ���� ������ �Ѵ�.
    }
    private void ApplyHealthChange()
    {
        AttackSO attackSO = Stats.CurrentStats.attackSO; //������ ���� ����
        bool hasBeenChanged = _collidingTargetHealthSystem.ChangeHealth(-attackSO.power); //����� �ｺ�ý��ۿ��� �ڽ��� ���ݷ¸�ŭ �������� ���Ѵ�.
        if (attackSO.isOnKnockback && _collidingMovement != null)// �ڽ��� ���� ������ �˹��� �ְ� ����� �̵� ��ũ��Ʈ�� �ִٸ�
        {
            _collidingMovement.ApplyKnockback(transform,attackSO.knockbackPower,attackSO.knockbackTime); //��븦 �˹� ��Ų��.
        }
        if (isTargetPlayer == true) //Ÿ���� �÷��̾���
        {
            GameManager.instance.ChangeHpBar(attackSO.power);//�÷��̾��� ü�¹ٸ� ���ҽ�Ų��.
        }
    }
}
