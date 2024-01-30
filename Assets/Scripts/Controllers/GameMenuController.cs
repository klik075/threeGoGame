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

    }
}
