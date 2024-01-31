using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnDeath : MonoBehaviour
{
    private HealthSystem _healthSystem;
    [SerializeField] private bool player = false;//script가 연결된 객체가 player 여부 체크
    private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _gameObject;

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
            color.a = 0.3f; //스프라이트에 투명도를 준다.
            renderer.color = color;
        }

        if (player == true) //죽는 객체가 player라면 gameManager에서 결과창 함수 호출
        {
            GameManager.instance.PopUpEnd();//종료 팝업을 띄운다. 
            Time.timeScale = 0.0f;//시간 정지
        }
        else //몬스터라면 
        {
            float exp = _gameObject.GetComponent<CharacterStatHandler>().CurrentStats.exp;
            GameManager.instance.ExpChange(exp); //플레이어에게 경험치를 준다.
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())//죽은 객체의 모든 컴포넌트를 정지시킨다.
        {
            component.enabled = false;
        }
        Destroy(gameObject, 2f); // 2초 후 삭제
    }
}