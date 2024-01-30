using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    private TimeManager _timeManager;
    public static GameMenuController menu;
    [SerializeField] private GameObject gameEndPopup;
    [SerializeField] private Text timeText;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;

    [SerializeField] private GameObject gameClearText;
    [SerializeField] private GameObject gameOverText;

    [SerializeField] private Text charactername1;
    [SerializeField] private Text charactername2;
    [SerializeField] private Text charactername3;

    [SerializeField] private Text score1;
    [SerializeField] private Text score2;
    [SerializeField] private Text score3;

    // Start is called before the first frame update
    void Start()
    {
        menu = this;
        _timeManager = TimeManager.timeIns;
        _timeManager.OnGameEnd += GameEnd;      // 게임 종료 이벤트 구독하기
        gameEndPopup.SetActive(false);      // 게임종료 팝업 창 비활성화 시켜놓기
        gameClearText.SetActive(false);
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickHomeButton()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameEnd(GameEndType gameEndType)
    {
        // 게임 종료 시 팝업창 띄우기
        switch (gameEndType)
        {
            // 게임 클리어 시 Game Clear
            case GameEndType.GameClear:
                gameClearText.SetActive(true);
                break;
            // 캐릭터 사망 시 Game Over
            case GameEndType.GameOver:
                gameOverText.SetActive(true);
                break;
        }

        gameEndPopup.SetActive(true);

        //기록들을 불러와서 현재 기록과 비교 후 상위 3개 저장 
        if (PlayerPrefs.HasKey("bestScore1") == true && PlayerPrefs.HasKey("bestScore2") == true && PlayerPrefs.HasKey("bestScore3") == true)
        {
            int temp1 = PlayerPrefs.GetInt("bestScore1");
            int temp2 = PlayerPrefs.GetInt("bestScore2");
            int temp3 = PlayerPrefs.GetInt("bestScore3");
            int temp4 = GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4;

            if(temp1>= temp4 && temp2 >= temp4 && temp3 >= temp4)
            {

            }
            else if(temp1 >= temp4 && temp2>= temp4 && temp4>temp3)
            {
                PlayerPrefs.SetString("bestScore3Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore3", GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4);
            }
            else if(temp1 >= temp4 && temp4>temp2 && temp4>temp3)
            {
                string tempS2= PlayerPrefs.GetString("bestScore2Name");

                PlayerPrefs.SetString("bestScore2Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore2", GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4);

                PlayerPrefs.SetString("bestScore3Name", tempS2);
                PlayerPrefs.SetInt("bestScore3", temp2);
            }
            else
            {
                string tempS1 = PlayerPrefs.GetString("bestScore1Name");
                string tempS2 = PlayerPrefs.GetString("bestScore2Name");

                PlayerPrefs.SetString("bestScore1Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore1", GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4);

                PlayerPrefs.SetString("bestScore2Name", tempS1);
                PlayerPrefs.SetInt("bestScore2", temp1);

                PlayerPrefs.SetString("bestScore3Name", tempS2);
                PlayerPrefs.SetInt("bestScore3", temp2);
            }

        }
        else if(PlayerPrefs.HasKey("bestScore1") == true && PlayerPrefs.HasKey("bestScore2") == true && PlayerPrefs.HasKey("bestScore3") == false)
        {
            int temp1 = PlayerPrefs.GetInt("bestScore1");
            int temp2 = PlayerPrefs.GetInt("bestScore2");
            int temp3 = GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4;

            if (temp1 >= temp3 && temp2 >= temp3)
            {
                PlayerPrefs.SetString("bestScore3Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore3", GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4);
            }
            else if(temp1 >= temp3 && temp3 > temp2)
            {
                string tempS1 = PlayerPrefs.GetString("bestScore2Name");

                PlayerPrefs.SetString("bestScore2Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore2", GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4);

                PlayerPrefs.SetString("bestScore3Name", tempS1);
                PlayerPrefs.SetInt("bestScore3", temp2);
            }
            else
            {
                string tempS1 = PlayerPrefs.GetString("bestScore1Name");
                string tempS2 = PlayerPrefs.GetString("bestScore2Name");

                PlayerPrefs.SetString("bestScore1Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore1", GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4);

                PlayerPrefs.SetString("bestScore2Name", tempS1);
                PlayerPrefs.SetInt("bestScore2", temp1);

                PlayerPrefs.SetString("bestScore3Name", tempS2);
                PlayerPrefs.SetInt("bestScore3", temp2);
            }

        }
        else if(PlayerPrefs.HasKey("bestScore1") == true && PlayerPrefs.HasKey("bestScore2") == false)
        {
            if(PlayerPrefs.GetInt("bestScore1") >= GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4)
            {
                PlayerPrefs.SetString("bestScore2Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore2", GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4);
            }
            else
            {
                string temp1 = PlayerPrefs.GetString("bestScore1Name");
                int temp2 = PlayerPrefs.GetInt("bestScore1");

                PlayerPrefs.SetString("bestScore1Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore1", GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4);

                PlayerPrefs.SetString("bestScore2Name", temp1);
                PlayerPrefs.SetInt("bestScore2", temp2);
            }
        }
        else 
        {
            PlayerPrefs.SetString("bestScore1Name", GameManager.instance.textname.text);
            PlayerPrefs.SetInt("bestScore1", GameManager.instance.enemyDefeatCount * 1 + (int)_timeManager.timeGoing * 4);
        }

        Invoke("display", 0.5f);             
    }

    void display()
    {
        //UI에 입력
        charactername1.text = PlayerPrefs.HasKey("bestScore1Name") ? PlayerPrefs.GetString("bestScore1Name") : "---";
        charactername2.text = PlayerPrefs.HasKey("bestScore2Name") ? PlayerPrefs.GetString("bestScore2Name") : "---";
        charactername3.text = PlayerPrefs.HasKey("bestScore3Name") ? PlayerPrefs.GetString("bestScore3Name") : "---";

        score1.text = (PlayerPrefs.HasKey("bestScore1") ? PlayerPrefs.GetInt("bestScore1") : 0).ToString();
        score2.text = (PlayerPrefs.HasKey("bestScore2") ? PlayerPrefs.GetInt("bestScore2") : 0).ToString();
        score3.text = (PlayerPrefs.HasKey("bestScore3") ? PlayerPrefs.GetInt("bestScore3") : 0).ToString();
    }
}
