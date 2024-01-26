using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 _aimDirection = Vector2.right;

    public GameObject testPrefab; // 추후삭.

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller.OnAttackEvent += OnShoot;
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection;
    }

    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackData rangedAttackData = attackSO as RangedAttackData;
        float projectilesAngleSpace = rangedAttackData.multipleProjectilesAngel;
        int numberOfProjextilesPerShot = rangedAttackData.numberofProjectilesPerShot;
        // 캐릭터가 발사하는 각도 부채꼴 모양 위로 올려줌 ?? 꺾어줌 ??  뭐 그런느낌 ...
        float minAngle = -(numberOfProjextilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackData.multipleProjectilesAngel;

        for (int i = 0; i<numberOfProjextilesPerShot; i++) // 반복문 으로 여러개 생
        {
            float angle = minAngle + projectilesAngleSpace * i; // 각도계산  
            float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);
            angle += randomSpread;

            CreateProjectile(rangedAttackData, angle);
        }
    }

    private void CreateProjectile(RangedAttackData rangedAttackData, float angle) // RangedAttackData rangedAttackData, float angle
    {
        Instantiate(testPrefab, projectileSpawnPosition.position, Quaternion.identity); // 추후삭제
    }
    //{
    //    _projectileManager.ShootBullet(
    //        projectileSpawnPosition.position,
    //        RotateVector2(_aimDirection, angle),
    //        rangedAttackData
    //        );
    //    if (shootingClip)
    //        SoundManager.PlayClip(shootingClip);
    //}

    // Update is called once per frame
    void Update()
    {

    }
}
