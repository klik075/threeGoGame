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
    public List<Vector3> enemyLocation = new List<Vector3>();
    
    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        playerobject.GetComponent<CharacterStatHandler>().name = PlayerPrefs.GetString("CharacterName");
        textname.text = playerobject.GetComponent<CharacterStatHandler>().name;
        Instantiate(map, new Vector3(0,0,0), Quaternion.identity);
        EnemyLocationSet();
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
            int enemyLocationlist = Random.Range(0,6);
            enemyInstance.transform.position = enemyLocation[enemyLocationlist];
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

    void EnemyLocationSet()
    {
        enemyLocation.Add(new Vector3(-5f, 8f,0));
        enemyLocation.Add(new Vector3(5f, 8f,0));
        enemyLocation.Add(new Vector3(-8f, 0f,0));
        enemyLocation.Add(new Vector3(8f, 0f,0));
        enemyLocation.Add(new Vector3(-5f, -8f,0));
        enemyLocation.Add(new Vector3(5f, -8f,0));
    }

    public void PopUpEnd()//마지막에 결과 표시
    {
        GameMenuController.menu.GameEnd(GameEndType.GameOver);
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
