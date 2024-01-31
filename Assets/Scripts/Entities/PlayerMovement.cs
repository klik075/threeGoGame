using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private CharacterStatHandler _stats;
    private Vector2 _movementDirection = Vector2.zero;//�����̴� ����
    private Rigidbody2D _rigidbody;
    private Vector2 _knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _stats = GetComponent<CharacterStatHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovement(_movementDirection);//�ڽ��� ���� �ӵ� ����
        if (knockbackDuration > 0.0f) //�˹� �ð��� �ִٸ�
        {
            knockbackDuration -= Time.fixedDeltaTime;//�ð��� �����Ѵ�.
        }
    }

    private void Move(Vector2 direction)//�����̴� ���� ����
    {
        _movementDirection = direction;
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        _knockback = -(other.position - transform.position).normalized * power;  //�˹��� ���ʹ� 1 * power ��븦 �ٶ󺸴� �ݴ� �������� �˹�
    }
    private void ApplyMovement(Vector2 direction)//�ڽ��� ���� �ӵ� ����
    {
        direction = direction * _stats.CurrentStats.speed; //������ ���⿡ �ڽ��� ���ǵ带 ���Ѵ�.
        if (knockbackDuration > 0.0f) //�˹� �ð��� �ִٸ� 
        {
            direction += _knockback; //�˹� ó��
        }
        _rigidbody.velocity = direction; //������ �ӵ� ����
    }
}
