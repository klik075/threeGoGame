using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedAttackController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask levelCollisionLayer;
    [SerializeField] private bool isTargetPlayer;
    private RangedAttackData _attackData;
    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private ProjectileManager _projectileManager;

    public bool fxOnDestory = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    
    private void Update()
    {
        if (!_isReady)
        {
            return;
        }
        
        _currentDuration += Time.deltaTime; 

        if (_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = _direction * _attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // layermask 연산부분 
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer))) // Level과 부딪치면 
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestory); //투사체 삭제
        }
        else if (_attackData.target.value == (_attackData.target.value | (1 << collision.gameObject.layer))) // 타겟과 부딪쳤다면
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>(); //상대의 헬스시스템을 가져오고
            if (healthSystem != null) // 널이 아니면
            {
                healthSystem.ChangeHealth(-_attackData.power); //체력을 자신의 공격력 만큼 닳게 한다.
                
                if (_attackData.isOnKnockback)//넉백이 있다면
                {
                    PlayerMovement movement = collision.GetComponent<PlayerMovement>();//상대방의 Move 컴포넌트를 가져오고
                    if (movement != null) //Move가 있다면
                    {
                        movement.ApplyKnockback(transform, _attackData.knockbackPower, _attackData.knockbackTime); //상대에게 넉백 적용
                    }
                }

                //타깃이 player라면 gamemanager에서 Hp바가 변경되는 작업 선언
                if(isTargetPlayer == true)
                {
                    GameManager.instance.ChangeHpBar(_attackData.power);
                }
                
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);//투사체 삭제
        }
    }


    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager) //투사체 초기화
    {
        _projectileManager = projectileManager;
        _attackData = attackData;
        _direction = direction;

        _trailRenderer.Clear();
        _currentDuration = 0;
        //_spriteRenderer.color = attackData.projectileColor; 없애야 함.

        transform.right = _direction;

        _isReady = true;
    }

    private void UpdateProjectilSprite()
    {
        transform.localScale = Vector3.one * _attackData.size;
    }

    
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {

        }
        gameObject.SetActive(false); 
    }
}
