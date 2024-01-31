using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private CharacterStatHandler _stats;
    private Vector2 _movementDirection = Vector2.zero;//움직이는 방향
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
        ApplyMovement(_movementDirection);//자신의 현재 속도 설정
        if (knockbackDuration > 0.0f) //넉백 시간이 있다면
        {
            knockbackDuration -= Time.fixedDeltaTime;//시간을 감소한다.
        }
    }

    private void Move(Vector2 direction)//움직이는 방향 설정
    {
        _movementDirection = direction;
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        _knockback = -(other.position - transform.position).normalized * power;  //넉백의 벡터는 1 * power 상대를 바라보는 반대 방향으로 넉백
    }
    private void ApplyMovement(Vector2 direction)//자신의 현재 속도 설정
    {
        direction = direction * _stats.CurrentStats.speed; //움직일 방향에 자신의 스피드를 곱한다.
        if (knockbackDuration > 0.0f) //넉백 시간이 있다면 
        {
            direction += _knockback; //넉백 처리
        }
        _rigidbody.velocity = direction; //현재의 속도 설정
    }
}
