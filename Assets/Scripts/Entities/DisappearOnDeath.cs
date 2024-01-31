using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnDeath : MonoBehaviour
{
    private HealthSystem _healthSystem;
    [SerializeField] private bool player = false;//script�� ����� ��ü�� player ���� üũ
    private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _gameObject;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthSystem.OnDeath += OnDeath; //���
    }

    void OnDeath()
    {
        _rigidbody.velocity = Vector3.zero; //������ �̵� 0

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>()) //���� ������ �� ������ ��������Ʈ
        {
            Color color = renderer.color;
            color.a = 0.3f; //��������Ʈ�� ������ �ش�.
            renderer.color = color;
        }

        if (player == true) //�״� ��ü�� player��� gameManager���� ���â �Լ� ȣ��
        {
            GameManager.instance.PopUpEnd();//���� �˾��� ����. 
            Time.timeScale = 0.0f;//�ð� ����
        }
        else //���Ͷ�� 
        {
            float exp = _gameObject.GetComponent<CharacterStatHandler>().CurrentStats.exp;
            GameManager.instance.ExpChange(exp); //�÷��̾�� ����ġ�� �ش�.
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())//���� ��ü�� ��� ������Ʈ�� ������Ų��.
        {
            component.enabled = false;
        }
        Destroy(gameObject, 2f); // 2�� �� ����
    }
}