using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private GameObject startGameMenu;      // �÷��̾� �̸� �Է�â
    [SerializeField] private Text textName;
    [SerializeField] Button gameTitlePlayButton;    // �÷��̾� �̸� �Է�â�� ���� Play ��ư
    [SerializeField] Button StartGameMenuPlayButton;    // �÷��̾� �̸� �Է�â���� MainScene�� �ε��ϴ� Play ��ư

    public void OnClickgameTitlePlayButton()
    {
        // �÷��̾� �̸� �Է�â ����
        startGameMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnClickStartGameMenuPlayButton()
    {
        // Play ��ư Ŭ�� �� InputField�� ���� �÷��̾��� �̸� ����
        string temp = textName.text.ToString();
        
        PlayerPrefs.SetString("CharacterName", temp);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainScene");
    }
}
