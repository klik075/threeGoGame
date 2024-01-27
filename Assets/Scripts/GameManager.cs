using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public enum GameEndType
{
    GameClear,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject playerobject;
    [SerializeField] private GameObject playerHpBar;
    [SerializeField] private Text textname;
    [SerializeField] private GameObject map;
    public PrefabManager prefabs;
    
    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        playerobject.GetComponent<CharacterStatHandler>().name = PlayerPrefs.GetString("CharacterName");
        textname.text = playerobject.GetComponent<CharacterStatHandler>().name;
        Instantiate(map, new Vector3(0,0,0), Quaternion.identity);
    }

    private void Start()
    { 
        Time.timeScale = 1f;
        InvokeRepeating("makeEnemy", 0.0f, 1.0f);//반복 호출

    }

    void makeEnemy()//적 객체 생성
    {
        if(playerobject.GetComponent<CharacterStatHandler>().CurrentStats.lv==1)
        {
            GameObject enemyInstance = Instantiate(prefabs.Enemy1Prefab);
            float x = Random.Range(-5f, 5f);
            float y = 8f;
            enemyInstance.transform.position = new Vector3(x, y, 0);
        }
        else if(playerobject.GetComponent<CharacterStatHandler>().CurrentStats.lv == 2)
        {
            Instantiate(prefabs.Enemy2Prefab);
        }
        else
        {
            Instantiate(prefabs.Enemy2Prefab);
        }

    }

    public void PopUpEnd()//마지막에 결과 표시
    {

    }

    public void ChangeHpBar(float attack)//받은 데미지에 따른 체력바 UI 변경
    {
        float maxHp = playerobject.GetComponent<HealthSystem>().MaxHealth;
        playerHpBar.transform.localScale += new Vector3(-attack / maxHp, 0, 0);
        if(playerHpBar.GetComponent<Transform>().localScale.x <= -1)
        {
            float y = playerHpBar.GetComponent<Transform>().localScale.y;
            float z = playerHpBar.GetComponent<Transform>().localScale.z;
            playerHpBar.transform.localScale = new Vector3(-1,y,z);
        }
    }
}
