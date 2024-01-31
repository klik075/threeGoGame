using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    [SerializeField] private Text timeText;
    float gameTime = 0f;

    private TimeManager timeManager;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        timeManager = TimeManager.timeIns;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = timeManager.timeGoing;
        // 현재 진행 시간 띄우기
        timeText.text = $"{(int)(gameTime / 60)}" + ":" + $"{(int)gameTime%60:D2}";
    }
}
