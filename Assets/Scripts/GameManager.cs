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
    public List<Vector3> enemyLocation = new List<Vector3>();//evemy start location list
    private HealthSystem healthSystem;

    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;//gets transform data of object(tag == player)
        healthSystem = Player.gameObject.GetComponent<HealthSystem>();
        playerobject.GetComponent<CharacterStatHandler>().name = PlayerPrefs.GetString("CharacterName");
        textname.text = "lv.1 " + playerobject.GetComponent<CharacterStatHandler>().name;//lv and name of player to text UI
        Instantiate(map, new Vector3(0,0,0), Quaternion.identity);//gets grid map from prefab
        EnemyLocationSet();//set enemy start location data
        healthSystem.OnDamage += PlayHitSFX;//input PlayHitSFX function in OnDamage event
    }

    private void Start()
    { 
        Time.timeScale = 1f;
        InvokeRepeating("makeEnemy", 0.0f, 1.0f);//반복 호출

    }

    protected void FixedUpdate()//every 1 second, lv of player to text UI
    {
        textname.text = "lv." + playerobject.GetComponent <CharacterStatHandler>().CurrentStats.lv + " " + playerobject.GetComponent<CharacterStatHandler>().name;
    }

    void makeEnemy()//enemy object create
    {
        int randomNumb = Random.Range(0, prefabs.EnemyNumber);
        GameObject enemyInstance = Instantiate(prefabs.EnemyList[randomNumb]);//prefab 복제하여 적 객체 생성
        int enemyLocationlist = Random.Range(0, 6);
        enemyInstance.transform.position = enemyLocation[enemyLocationlist];

        enemyInstance.GetComponent<CharacterStatHandler>().CurrentStats.attackSO.power += playerobject.GetComponent<CharacterStatHandler>().CurrentStats.lv;
    }

    void EnemyLocationSet()//enemy start location set
    {
        enemyLocation.Add(new Vector3(-5f, 8f,0));
        enemyLocation.Add(new Vector3(5f, 8f,0));
        enemyLocation.Add(new Vector3(-8f, 0f,0));
        enemyLocation.Add(new Vector3(8f, 0f,0));
        enemyLocation.Add(new Vector3(-5f, -8f,0));
        enemyLocation.Add(new Vector3(5f, -8f,0));
    }

    public void PopUpEnd()//result UI
    {
        GameMenuController.menu.GameEnd(GameEndType.GameOver);
        DataSaveAndLoad();   
    }

    public void ChangeHpBar(float attack)//damage -> hp bar UI change
    {
        float maxHp = healthSystem.MaxHealth;//gets max hp
        playerHpBar.transform.localScale = new Vector3(-((maxHp - healthSystem.CurrentHealth) / maxHp), 1, 1);//hp bar UI change
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

    private void PlayHitSFX()//hit 효과음 제공
    {
        AudioManager.instance.PlayClip(SFXClipType.Hit);
    }

    public void ExpChange(float exp)//enemy death -> exp earned -> change applied
    {  
        playerobject.GetComponent<CharacterStatHandler>().CurrentStats.exp += exp;//exp earned
        enemyDefeatCount++;//count that effects result score

        float currentExp = playerobject.GetComponent<CharacterStatHandler>().CurrentStats.exp;
        float maxExp = playerobject.GetComponent<CharacterStatHandler>().CurrentStats.fullExp;//gets max exp
        if (currentExp >= maxExp)//exp is bigger than max exp -> level up, stat change, hp recovery, exp to 0
        {
            playerobject.GetComponent<CharacterStatHandler>().CurrentStats.exp = 0;
            playerobject.GetComponent<CharacterStatHandler>().CurrentStats.lv++;
            playerobject.GetComponent<CharacterStatHandler>().CurrentStats.attackSO.power++;
            playerobject.GetComponent<HealthSystem>().ChangeHealth(5);

            ChangeHpBar(-5);//bar change code gets attack damage, so to become positive effect, 매개변수 has to be -

            AudioManager.instance.PlayClip(SFXClipType.LevelUp);
        }
    }

    public void DataSaveAndLoad()//player data save and load, gets 1,2,3 top score
    {

        //gets score data and compare with player's current record 
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
        

        //result 1,2,3 score data display
        charactername1.text = PlayerPrefs.HasKey("bestScore1Name") ? PlayerPrefs.GetString("bestScore1Name") : "---";
        charactername2.text = PlayerPrefs.HasKey("bestScore2Name") ? PlayerPrefs.GetString("bestScore2Name") : "---";
        charactername3.text = PlayerPrefs.HasKey("bestScore3Name") ? PlayerPrefs.GetString("bestScore3Name") : "---";

        score1.text = (PlayerPrefs.HasKey("bestScore1") ? PlayerPrefs.GetInt("bestScore1") : 0).ToString();
        score2.text = (PlayerPrefs.HasKey("bestScore2") ? PlayerPrefs.GetInt("bestScore2") : 0).ToString();
        score3.text = (PlayerPrefs.HasKey("bestScore3") ? PlayerPrefs.GetInt("bestScore3") : 0).ToString();
    }
}
