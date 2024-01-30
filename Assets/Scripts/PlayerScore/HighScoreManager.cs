using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    [SerializeField] private Text survivedText;
    private int _monsterKillCounter;
    private int _playerScore = 0;

    public List<PlayerHighScore> playerHighScoreList = new List<PlayerHighScore>();     // TODO : HighScore_Data.Json 파일 만들어서 읽어오기

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 게임 종료 시 CalcHighScore 실행
        GameManager.instance.OnGameOver += CalcHighScore;
        TimeManager.timeIns.OnGameEnd += CalcHighScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CalcHighScore(GameEndType type)
    {
        int playerSurvivedSecond = 0;   // 생존시간 deltaTime을 초 단위로 변경하기

        if (survivedText != null && int.TryParse(survivedText.text, out playerSurvivedSecond))
        {
            playerSurvivedSecond %= 60;
        }
        else return;

        _monsterKillCounter = GameManager.instance.monsterKillCounter;
        _playerScore = _monsterKillCounter + playerSurvivedSecond;  // 점수 계산

        SetNewHighScore();
    }

    private void SetNewHighScore()
    {
        if (playerHighScoreList.Count < 10)
        {
            PlayerHighScore newHighScore = new PlayerHighScore(PlayerPrefs.GetString("CharacterName"), _playerScore);
            playerHighScoreList.Add(newHighScore);
            playerHighScoreList.Sort();
        }
        else
        {
            int index = playerHighScoreList.FindIndex(i => (i.highScore <= _playerScore));

            if (index != -1)
            {
                PlayerHighScore newHighScore = new PlayerHighScore(PlayerPrefs.GetString("CharacterName"), _playerScore);
                playerHighScoreList[index] = newHighScore;
                playerHighScoreList.Sort();
            }
        }
    }
}
