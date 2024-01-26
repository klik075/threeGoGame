using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private ProjectileManager _projectileManager;
    private CharacterController _controller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 _aimDirection = Vector2.right;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("start");
        _projectileManager = ProjectileManager.instance;
        _controller.OnAttackEvent += OnShoot;
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection;
    }

    private void OnShoot(AttackSO attackSO) 
    {
        {
            RangedAttackData rangedAttackData = attackSO as RangedAttackData;
            float projectilesAngleSpace = rangedAttackData.multipleProjectilesAngel;
            int numberOfProjextilesPerShot = rangedAttackData.numberofProjectilesPerShot;
            
            float minAngle = -(numberOfProjextilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackData.multipleProjectilesAngel;

            //Debug.LogError(numberOfProjextilesPerShot);
            for (int i = 0; i < numberOfProjextilesPerShot; i++)    
            {

                float angle = minAngle + projectilesAngleSpace * i; 
                float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);
                angle += randomSpread;

                CreateProjectile(rangedAttackData, angle);
            }
        }
    }
    

    private void CreateProjectile(RangedAttackData rangedAttackData, float angle) 
    {
        _projectileManager.ShootBullet(
            projectileSpawnPosition.position,
            RotateVector2(_aimDirection, angle),
            rangedAttackData
            );
        //if (shootingClip)
        //    SoundManager.PlayClip(shootingClip);
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }


}
