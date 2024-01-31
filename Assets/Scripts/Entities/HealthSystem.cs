using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .1f;//체력 변화 가능 시간

    private CharacterStatHandler _statsHandler;
    private float _timeSinceLastChange = float.MaxValue;

    public event Action OnDamage; //데미지를 받았을 때
    public event Action OnHeal; //체력 회복했을 때
    public event Action OnDeath;//죽었을 때
    public event Action OnInvincibilityEnd;//피격 후 무적 끝났을 때

    public float CurrentHealth { get; set; }//현재의 체력

    public float MaxHealth => _statsHandler.CurrentStats.maxHealth;//최대 체력

    private void Awake()
    {
        _statsHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statsHandler.CurrentStats.maxHealth;//처음 생성시 현재 체력을 최대 체력으로 설정
    }

    private void Update()
    {
        if (_timeSinceLastChange < healthChangeDelay)//체력 변화 시간보다 작다면
        {
            _timeSinceLastChange += Time.deltaTime; //시간을 더하고
            if (_timeSinceLastChange >= healthChangeDelay)//크다면
            {
                OnInvincibilityEnd?.Invoke();//무적 끝났다고 알림.
            }
        }
    }


    public bool ChangeHealth(float change)//캐릭터의 체력 변동
    {
        if (change == 0 || _timeSinceLastChange < healthChangeDelay) //변화가 없거나 다음 피격 시간이 안 되었으면
        {
            return false;//변화를 제대로 하지 못하면
        }

        _timeSinceLastChange = 0f;//시간을 초기화
        CurrentHealth += change;//체력 변동
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;// 최대체력을 넘지 못하게 설정
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;//체력을 0보다 작지 않게 설정

        if (change > 0) //변화가 + 라면
        {
            OnHeal?.Invoke(); //힐
        }
        else
        {
            OnDamage?.Invoke();//데미지 받는다
        }

        if (CurrentHealth <= 0f) //현재 체력이 0보다 작으면
        {
            CallDeath(); //죽었다.
        }

        return true;//변화를 제대로 완료하면 true
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}