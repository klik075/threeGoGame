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
    [SerializeField] public Text textname;
    [SerializeField] private GameObject map;

    [SerializeField] private Text charactername1;
    [SerializeField] private Text charactername2;
    [SerializeField] private Text charactername3;

    [SerializeField] private Text score1;
    [SerializeField] private Text score2;
    [SerializeField] private Text score3;

    public int enemyDefeatCount=0;
    public PrefabManager prefabs;
    public List<Vector3> enemyLocation = new List<Vector3>();
    private HealthSystem healthSystem;

    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        healthSystem = Player.gameObject.GetComponent<HealthSystem>();
        playerobject.GetComponent<CharacterStatHandler>().name = PlayerPrefs.GetString("CharacterName");
        textname.text = "lv.1 " + playerobject.GetComponent<CharacterStatHandler>().name;
        Instantiate(map, new Vector3(0,0,0), Quaternion.identity);
        EnemyLocationSet();
    }

    private void Start()
    { 
        Time.timeScale = 1f;
        InvokeRepeating("makeEnemy", 0.0f, 1.0f);//반복 호출

    }

    protected void FixedUpdate()
    {
        textname.text = "lv." + playerobject.GetComponent <CharacterStatHandler>().CurrentStats.lv + " " + playerobject.GetComponent<CharacterStatHandler>().name;
    }

    void makeEnemy()//적 객체 생성
    {
        int randomNumb = Random.Range(0, prefabs.EnemyNumber);
        GameObject enemyInstance = Instantiate(prefabs.EnemyList[randomNumb]);
        int enemyLocationlist = Random.Range(0, 6);
        enemyInstance.transform.position = enemyLocation[enemyLocationlist];

        enemyInstance.GetComponent<CharacterStatHandler>().CurrentStats.attackSO.power += playerobject.GetComponent<CharacterStatHandler>().CurrentStats.lv;
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
        DataSaveAndLoad();

        
    }

    public void ChangeHpBar(float attack)//받은 데미지에 따른 체력바 UI 변경
    {
        //float maxHp = playerobject.GetComponent<HealthSystem>().MaxHealth;
        //playerHpBar.transform.localScale = new Vector3(-attack / maxHp, 0, 0);
        float maxHp = healthSystem.MaxHealth;
        playerHpBar.transform.localScale = new Vector3(-((maxHp - healthSystem.CurrentHealth) / maxHp), 1, 1);
        if (playerHpBar.GetComponent<Transform>().localScale.x <= -1)
        {
            float y = playerHpBar.GetComponent<Transform>().localScale.y;
            float z = playerHpBar.GetComponent<Transform>().localScale.z;
            playerHpBar.transform.localScale = new Vector3(-1,y,z);
        }
        else if(playerHpBar.GetComponent<Transform>().localScale.x >= 0)
        {
            float y = playerHpBar.GetComponent<Transform>().localScale.y;
            float z = playerHpBar.GetComponent<Transform>().localScale.z;
            playerHpBar.transform.localScale = new Vector3(0, y, z);
        }
    }

    public void ExpChange(float exp)
    {  
        playerobject.GetComponent<CharacterStatHandler>().CurrentStats.exp += exp;
        enemyDefeatCount++;

        float currentExp = playerobject.GetComponent<CharacterStatHandler>().CurrentStats.exp;
        float maxExp = playerobject.GetComponent<CharacterStatHandler>().CurrentStats.fullExp;
        if (currentExp >= maxExp)
        {
            playerobject.GetComponent<CharacterStatHandler>().CurrentStats.exp = 0;
            playerobject.GetComponent<CharacterStatHandler>().CurrentStats.lv++;
            playerobject.GetComponent<CharacterStatHandler>().CurrentStats.attackSO.power++;
            playerobject.GetComponent<HealthSystem>().ChangeHealth(5);
            ChangeHpBar(-5);
        }
    }

    public void DataSaveAndLoad()
    {

        //기록들을 불러와서 현재 기록과 비교 후 상위 3개 저장 
        if (PlayerPrefs.HasKey("bestScore1") == true && PlayerPrefs.HasKey("bestScore2") == true && PlayerPrefs.HasKey("bestScore3") == true)
        {
            int temp1 = PlayerPrefs.GetInt("bestScore1");
            int temp2 = PlayerPrefs.GetInt("bestScore2");
            int temp3 = PlayerPrefs.GetInt("bestScore3");
            int temp4 = GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4;

            if (temp1 >= temp4 && temp2 >= temp4 && temp3 >= temp4)
            {

            }
            else if (temp1 >= temp4 && temp2 >= temp4 && temp4 > temp3)
            {
                PlayerPrefs.SetString("bestScore3Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore3", GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4);
            }
            else if (temp1 >= temp4 && temp4 > temp2 && temp4 > temp3)
            {
                string tempS2 = PlayerPrefs.GetString("bestScore2Name");

                PlayerPrefs.SetString("bestScore2Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore2", GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4);

                PlayerPrefs.SetString("bestScore3Name", tempS2);
                PlayerPrefs.SetInt("bestScore3", temp2);
            }
            else
            {
                string tempS1 = PlayerPrefs.GetString("bestScore1Name");
                string tempS2 = PlayerPrefs.GetString("bestScore2Name");

                PlayerPrefs.SetString("bestScore1Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore1", GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4);

                PlayerPrefs.SetString("bestScore2Name", tempS1);
                PlayerPrefs.SetInt("bestScore2", temp1);

                PlayerPrefs.SetString("bestScore3Name", tempS2);
                PlayerPrefs.SetInt("bestScore3", temp2);
            }

        }
        else if (PlayerPrefs.HasKey("bestScore1") == true && PlayerPrefs.HasKey("bestScore2") == true && PlayerPrefs.HasKey("bestScore3") == false)
        {
            int temp1 = PlayerPrefs.GetInt("bestScore1");
            int temp2 = PlayerPrefs.GetInt("bestScore2");
            int temp3 = GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4;

            if (temp1 >= temp3 && temp2 >= temp3)
            {
                PlayerPrefs.SetString("bestScore3Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore3", GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4);
            }
            else if (temp1 >= temp3 && temp3 > temp2)
            {
                string tempS1 = PlayerPrefs.GetString("bestScore2Name");

                PlayerPrefs.SetString("bestScore2Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore2", GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4);

                PlayerPrefs.SetString("bestScore3Name", tempS1);
                PlayerPrefs.SetInt("bestScore3", temp2);
            }
            else
            {
                string tempS1 = PlayerPrefs.GetString("bestScore1Name");
                string tempS2 = PlayerPrefs.GetString("bestScore2Name");

                PlayerPrefs.SetString("bestScore1Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore1", GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4);

                PlayerPrefs.SetString("bestScore2Name", tempS1);
                PlayerPrefs.SetInt("bestScore2", temp1);

                PlayerPrefs.SetString("bestScore3Name", tempS2);
                PlayerPrefs.SetInt("bestScore3", temp2);
            }

        }
        else if (PlayerPrefs.HasKey("bestScore1") == true && PlayerPrefs.HasKey("bestScore2") == false)
        {
            if (PlayerPrefs.GetInt("bestScore1") >= GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4)
            {
                PlayerPrefs.SetString("bestScore2Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore2", GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4);
            }
            else
            {
                string temp1 = PlayerPrefs.GetString("bestScore1Name");
                int temp2 = PlayerPrefs.GetInt("bestScore1");

                PlayerPrefs.SetString("bestScore1Name", GameManager.instance.textname.text);
                PlayerPrefs.SetInt("bestScore1", GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4);

                PlayerPrefs.SetString("bestScore2Name", temp1);
                PlayerPrefs.SetInt("bestScore2", temp2);
            }
        }
        else
        {
            PlayerPrefs.SetString("bestScore1Name", GameManager.instance.textname.text);
            PlayerPrefs.SetInt("bestScore1", GameManager.instance.enemyDefeatCount * 1 + (int)TimeManager.timeIns.timeGoing * 4);
        }
        Debug.Log(PlayerPrefs.GetString("bestScore1Name"));

        Debug.Log(charactername1.text);
        charactername1.text = PlayerPrefs.HasKey("bestScore1Name") ? PlayerPrefs.GetString("bestScore1Name") : "---";
        Debug.Log(charactername1.text);
        charactername2.text = PlayerPrefs.HasKey("bestScore2Name") ? PlayerPrefs.GetString("bestScore2Name") : "---";
        charactername3.text = PlayerPrefs.HasKey("bestScore3Name") ? PlayerPrefs.GetString("bestScore3Name") : "---";

        score1.text = (PlayerPrefs.HasKey("bestScore1") ? PlayerPrefs.GetInt("bestScore1") : 0).ToString();
        score2.text = (PlayerPrefs.HasKey("bestScore2") ? PlayerPrefs.GetInt("bestScore2") : 0).ToString();
        score3.text = (PlayerPrefs.HasKey("bestScore3") ? PlayerPrefs.GetInt("bestScore3") : 0).ToString();
    }
}
