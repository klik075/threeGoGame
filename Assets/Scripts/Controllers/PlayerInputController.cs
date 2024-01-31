using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : CharacterController
{
    private Camera _camera;
    [SerializeField] Vector2 minCameraBoundary; //최소 이동 수치
    [SerializeField] Vector2 maxCameraBoundary; // 최대 이동 수치

    protected override void Awake() 
    {
        base.Awake(); 
        _camera = Camera.main; //메인 카메라 연결
    }

    protected override void FixedUpdate()//부모 클래스에서 virtual로 선언했기에 override하여 FixedUpdate 선언, 지속적으로 위치값을 메인카메라와 연결
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, -10);//연결되어 있는 gameobject의 위치값 인식

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x); //mathf.clamp는 최소,최대 비교후 사이값이면 그대로 출력
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        _camera.transform.position = new Vector3(targetPos.x, targetPos.y, -10); //카메라 위치값 적용
    }

    public void OnMove(InputValue value)//wasd를 눌렀을 때 실행
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;//wasd에 해당하는 벡터를 단위 벡터로 변환
        CallMoveEvent(moveInput);//움직임 실행
    }

    public void OnLook(InputValue value)//마우스를 화면상에서 움직였을 때
    {
        Vector2 newAim = value.Get<Vector2>();//스크린상 좌표를 가져오고
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);//게임좌표로 변환한다.
        newAim = (worldPos - (Vector2)transform.position).normalized;//자신의 위치에서 에임의 위치로 가는 단위벡터를 반환

        if (newAim.magnitude >= .9f)//크기가 .9보다 클 때
        {
            CallLookEvent(newAim);//실행
        }
    }

    public void OnFire(InputValue value)//마우스를 눌렀을 때 실행
    {
        IsAttacking = value.isPressed; //누르면 공격 가능으로 변경
    }
}
