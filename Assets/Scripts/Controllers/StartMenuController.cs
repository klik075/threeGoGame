using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private GameObject startGameMenu;
    [SerializeField] private Text text;
    [SerializeField] Button gameTitlePlayButton;
    [SerializeField] Button StartGameMenuPlayButton;

    public void OnClickgameTitlePlayButton()
    {
        startGameMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnClickStartGameMenuPlayButton()
    {
        string temp = text.ToString();
        // TODO : 캐릭터 생성
        PlayerPrefs.SetString("CharacterName", temp);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainScene");
    }
}
