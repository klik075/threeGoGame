using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private GameObject startGameMenu;      // 플레이어 이름 입력창
    [SerializeField] private Text textName;
    [SerializeField] Button gameTitlePlayButton;    // 플레이어 이름 입력창을 띄우는 Play 버튼
    [SerializeField] Button StartGameMenuPlayButton;    // 플레이어 이름 입력창에서 MainScene을 로드하는 Play 버튼

    public void OnClickgameTitlePlayButton()
    {
        // 플레이어 이름 입력창 띄우기
        startGameMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnClickStartGameMenuPlayButton()
    {
        // Play 버튼 클릭 시 InputField에 적힌 플레이어의 이름 저장
        string temp = textName.text.ToString();
        
        PlayerPrefs.SetString("CharacterName", temp);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainScene");
    }
}
