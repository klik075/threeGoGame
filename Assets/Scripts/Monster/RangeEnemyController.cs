using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyController : EnemyController
{
    [SerializeField] private float followRange = 1000f; //플레이어 인지 범위
    [SerializeField] private float shootRange = 10f; //사정거리

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        IsAttacking = false;
        if (distance <= followRange)
        {
            if (distance <= shootRange)
            {
                int layerMaskTarget = Stats.CurrentStats.attackSO.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 11f, (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget); // 레이케스트는 땅과 플레이어의 합인 비트마스크

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer))) //레이와 처음 부딪힌 것이 있고 목표 비트마스크와 상대의 비트마스크가 같으면 (= 동일한 레이어라면)
                {
                    CallLookEvent(direction); //플레이어를 바라본다.
                    CallMoveEvent(Vector2.zero); //제자리에 멈춰서 쏜다.
                    IsAttacking = true;
                }
                else
                {
                    
                }
            }
            else
            {
                CallLookEvent(direction);//추가
                CallMoveEvent(direction);
            }
        }
        else
        {
            CallLookEvent(direction);//추가
            CallMoveEvent(direction);
        }
    }
}
