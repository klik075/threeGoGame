using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenuController : MonoBehaviour
{
    [SerializeField] private Button SettingButton;      // �ɼ�â�� ���� ��ư

    [SerializeField] private Button SettingHomeButton;
    [SerializeField] private Button SettingConfirmButton;

    [SerializeField] private GameObject SettingPopup;   // �ɼ� â

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
        // �ɼ� ��ư Ŭ�� �� �ɼ� â ����
        SettingPopup.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClickSettingHomeButton()
    {
        // Ȩ ��ư Ŭ�� �� StartScene���� �̵�
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickGameExitButton()
    {
        // StartScene�� �ɼ� â���� ���� ��ư Ŭ�� ��, ���� �����ϱ�
        Application.Quit();
    }

    public void OnClickSettingConfirmButton()
    {
        // �ɼ� â���� Ȯ�� ��ư Ŭ�� �� ���� ����
        Time.timeScale = 1f;
        SettingPopup.SetActive(false);
    }
}
