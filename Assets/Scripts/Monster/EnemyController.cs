using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour //CharacterController
{
    protected Transform ClosestTarget { get ; private set; }

    //protected override void Awake()
    //{
    //    base.Awake();
    //}
    protected virtual void Start()
    {
        
    }
    protected virtual void FixedUpdate()
    { 

    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }
    protected Vector2 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
    }
}
