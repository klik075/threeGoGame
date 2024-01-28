using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnDeath : MonoBehaviour
{
    private HealthSystem _healthSystem;
    [SerializeField] private bool player = false;//script가 연결된 객체가 player 여부 체크
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthSystem.OnDeath += OnDeath; //등록
    }

    void OnDeath()
    {
        _rigidbody.velocity = Vector3.zero; //죽으면 이동 0

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>()) //나를 포함한 내 하위의 스프라이트
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        if (player == true) //죽는 객체가 player라면 gameManager에서 결과창 함수 호출
        {
            Time.timeScale = 0.0f;
            GameManager.instance.PopUpEnd();
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())//
        {
            component.enabled = false;
        }
        Destroy(gameObject, 2f); // 2초 후 삭제
    }
}