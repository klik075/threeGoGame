using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager timeIns;
    public bool timeStop = true;

    public float timeGoing;

    private void Awake()
    {
        timeIns = this;//싱글톤화
        Time.timeScale = 0.0f;//timer 아직 시작 안하도록 지정
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(timeStop == false)
        {
            timeGoing += Time.deltaTime;
        }
    }

    public void StopTimer()
    {
        timeStop = true;
    }

    public void StartTimer()
    {
        timeStop = false;
        Time.timeScale = 1f;
    }
}
