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
        GameManager.instance.OnGameOver += CalcHighScore;
        TimeManager.timeIns.OnGameEnd += CalcHighScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CalcHighScore(GameEndType type)
    {
        int playerSurvivedSecond = 0;

        if (survivedText != null && int.TryParse(survivedText.text, out playerSurvivedSecond))
        {
            playerSurvivedSecond %= 60;
        }
        else return;

        _monsterKillCounter = GameManager.instance.monsterKillCounter;
        _playerScore = _monsterKillCounter + playerSurvivedSecond;

        SetNewHighScore();
    }

    private void SetNewHighScore()
    {
        int index = playerHighScoreList.FindIndex(i => i.highScore == _playerScore);

        if(index != -1)
        {
            PlayerHighScore newHighScore = new PlayerHighScore(PlayerPrefs.GetString("CharacterName"), _playerScore);
            playerHighScoreList[index] = newHighScore;
        }
    }
}
