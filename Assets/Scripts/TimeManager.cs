using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager timeIns;
    public bool timeStop = true;

    public event Action<GameEndType> OnGameEnd;  // 제한 시간 초과 시 GameEnd 이벤트 발생

    public float timeGoing=0;
    private float timeLimit = 6000;   // 제한 시간 10분 (임시)

    private void Awake()
    {
        timeIns = this;//싱글톤화
        Time.timeScale = 0.0f;//timer 아직 시작 안하도록 지정
    }
    void Start()
    {
        StartTimer();   // 게임 내 시간 흐르게 하기
    }

    void Update()
    {
        if(timeStop == false)
        {
            timeGoing += Time.deltaTime;
        }
        if (timeGoing >= timeLimit)     // 현재 시간이 제한 시간보다 커지면
        {
            OnGameEnd?.Invoke(GameEndType.GameClear);    // 게임 종료 이벤트 발생
            StopTimer();    // 게임 내 시간 멈추기
        }
    }

    public void StopTimer()
    {
        timeStop = true;
        Time.timeScale = 0f;
    }

    public void StartTimer()
    {
        timeStop = false;
        Time.timeScale = 1f;
    }
}
