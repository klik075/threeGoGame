using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenuController : MonoBehaviour
{
    [SerializeField] private Button SettingButton;

    [SerializeField] private Button SettingHomeButton;
    [SerializeField] private Button SettingConfirmButton;

    [SerializeField] private GameObject SettingPopup;

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
        SettingPopup.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClickGameExitButton()
    {
        Application.Quit();
    }

    public void OnClickSettingConfirmButton()
    {
        Time.timeScale = 1f;
        SettingPopup.SetActive(false);
    }
}
