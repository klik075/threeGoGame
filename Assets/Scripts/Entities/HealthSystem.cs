using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .1f;//ü�� ��ȭ ���� �ð�

    private CharacterStatHandler _statsHandler;
    private float _timeSinceLastChange = float.MaxValue;

    public event Action OnDamage; //�������� �޾��� ��
    public event Action OnHeal; //ü�� ȸ������ ��
    public event Action OnDeath;//�׾��� ��
    public event Action OnInvincibilityEnd;//�ǰ� �� ���� ������ ��

    public float CurrentHealth { get; set; }//������ ü��

    public float MaxHealth => _statsHandler.CurrentStats.maxHealth;//�ִ� ü��

    private void Awake()
    {
        _statsHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statsHandler.CurrentStats.maxHealth;//ó�� ������ ���� ü���� �ִ� ü������ ����
    }

    private void Update()
    {
        if (_timeSinceLastChange < healthChangeDelay)//ü�� ��ȭ �ð����� �۴ٸ�
        {
            _timeSinceLastChange += Time.deltaTime; //�ð��� ���ϰ�
            if (_timeSinceLastChange >= healthChangeDelay)//ũ�ٸ�
            {
                OnInvincibilityEnd?.Invoke();//���� �����ٰ� �˸�.
            }
        }
    }


    public bool ChangeHealth(float change)//ĳ������ ü�� ����
    {
        if (change == 0 || _timeSinceLastChange < healthChangeDelay) //��ȭ�� ���ų� ���� �ǰ� �ð��� �� �Ǿ�����
        {
            return false;//��ȭ�� ����� ���� ���ϸ�
        }

        _timeSinceLastChange = 0f;//�ð��� �ʱ�ȭ
        CurrentHealth += change;//ü�� ����
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;// �ִ�ü���� ���� ���ϰ� ����
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;//ü���� 0���� ���� �ʰ� ����

        if (change > 0) //��ȭ�� + ���
        {
            OnHeal?.Invoke(); //��
        }
        else
        {
            OnDamage?.Invoke();//������ �޴´�
        }

        if (CurrentHealth <= 0f) //���� ü���� 0���� ������
        {
            CallDeath(); //�׾���.
        }

        return true;//��ȭ�� ����� �Ϸ��ϸ� true
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}