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

    public void OnMove(InputValue value)
    {
        //Debug.Log("OnMove" + value.ToString());
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}
