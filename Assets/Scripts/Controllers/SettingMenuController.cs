using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenuController : MonoBehaviour
{
    [SerializeField] private Button SettingButton;      // 옵션창을 띄우는 버튼

    [SerializeField] private Button SettingHomeButton;
    [SerializeField] private Button SettingConfirmButton;

    [SerializeField] private GameObject SettingPopup;   // 옵션 창

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSettingButton()
    {
        // 옵션 버튼 클릭 시 옵션 창 띄우기
        SettingPopup.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClickSettingHomeButton()
    {
        // 홈 버튼 클릭 시 StartScene으로 이동
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickGameExitButton()
    {
        // StartScene의 옵션 창에서 종료 버튼 클릭 시, 게임 종료하기
        Application.Quit();
    }

    public void OnClickSettingConfirmButton()
    {
        // 옵션 창에서 확인 버튼 클릭 시 게임 진행
        Time.timeScale = 1f;
        SettingPopup.SetActive(false);
    }
}
