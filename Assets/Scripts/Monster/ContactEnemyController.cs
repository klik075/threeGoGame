using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContactEnemyController : EnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField] private string targetTag = "Player";
    private bool _isCollidingWithTarget;

    [SerializeField] private SpriteRenderer characterRenderer;
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

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
}
