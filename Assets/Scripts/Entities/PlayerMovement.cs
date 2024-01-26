using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private CharacterStatHandler _stats;
    private Vector2 _movementDirection = Vector2.zero;
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
        ApplyMovement(_movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        _knockback = -(other.position - transform.position).normalized * power;  //넉백의 벡터는 1 * power 상대를 바라보는 반대 방향으로 넉백
    }
    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * _stats.CurrentStats.speed;
        if (knockbackDuration > 0.0f) //넉백 시간이 있다면 
        {
            direction += _knockback; //넉백 처리
        }
        _rigidbody.velocity = direction;
    }
}
