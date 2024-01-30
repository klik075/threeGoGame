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
        _timeManager.OnGameEnd += GameEnd;      // ���� ���� �̺�Ʈ �����ϱ�
        gameEndPopup.SetActive(false);      // �������� �˾� â ��Ȱ��ȭ ���ѳ���
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
        // ���� ���� �� �˾�â ����
        switch (gameEndType)
        {
            // ���� Ŭ���� �� Game Clear
            case GameEndType.GameClear:
                gameClearText.SetActive(true);
                break;
            // ĳ���� ��� �� Game Over
            case GameEndType.GameOver:
                gameOverText.SetActive(true);
                break;
        }

        gameEndPopup.SetActive(true);

    }
}
