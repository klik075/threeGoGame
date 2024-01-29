using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyController : EnemyController
{
    [SerializeField] private float followRange = 1000f; //�÷��̾� ���� ����
    [SerializeField] private float shootRange = 10f; //�����Ÿ�

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
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 11f, (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget); // �����ɽ�Ʈ�� ���� �÷��̾��� ���� ��Ʈ����ũ

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer))) //���̿� ó�� �ε��� ���� �ְ� ��ǥ ��Ʈ����ũ�� ����� ��Ʈ����ũ�� ������ (= ������ ���̾���)
                {
                    CallLookEvent(direction); //�÷��̾ �ٶ󺻴�.
                    CallMoveEvent(Vector2.zero); //���ڸ��� ���缭 ���.
                    IsAttacking = true;
                }
                else
                {
                    
                }
            }
            else
            {
                CallLookEvent(direction);//�߰�
                CallMoveEvent(direction);
            }
        }
        else
        {
            CallLookEvent(direction);//�߰�
            CallMoveEvent(direction);
        }
    }
}
