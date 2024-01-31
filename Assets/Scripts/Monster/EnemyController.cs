using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    GameManager gameManager;
    protected Transform ClosestTarget { get ; private set; } //플레이어의 위치 참조

    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void Start()
    {
        gameManager = GameManager.instance;
        ClosestTarget = gameManager.Player;
    }
    protected override void FixedUpdate()
    { 
        base.FixedUpdate();
    }
    protected float DistanceToTarget() //플레이어와의 거리
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }
    protected Vector2 DirectionToTarget()//플레이어를 향한 방향
    {
        return (ClosestTarget.position - transform.position).normalized;
    }
}
