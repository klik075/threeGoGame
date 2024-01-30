using System;
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
    private HealthSystem healthSystem;

    public event Action<GameEndType> OnGameOver;
    public int monsterKillCounter = 0;

    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        healthSystem = Player.gameObject.GetComponent<HealthSystem>();
        playerobject.GetComponent<CharacterStatHandler>().name = PlayerPrefs.GetString("CharacterName");
        textname.text = "lv.1 " + playerobject.GetComponent<CharacterStatHandler>().name;
        Instantiate(map, new Vector3(0,0,0), Quaternion.identity);
        EnemyLocationSet();
        healthSystem.OnDamage += PlayHitSFX;
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
        int randomNumb = UnityEngine.Random.Range(0, prefabs.EnemyNumber);
        GameObject enemyInstance = Instantiate(prefabs.EnemyList[randomNumb]);
        int enemyLocationlist = UnityEngine.Random.Range(0, 6);
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
        //GameMenuController.menu.GameEnd(GameEndType.GameOver);
        OnGameOver?.Invoke(GameEndType.GameOver);
    }

    public void ChangeHpBar(float attack)//받은 데미지에 따른 체력바 UI 변경
    {
        //if(attack != 5 && healthSystem.) AudioManager.instance.PlayClip(SFXClipType.Hit);

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
    }

    private void PlayHitSFX()
    {
        AudioManager.instance.PlayClip(SFXClipType.Hit);
    }

    public void ExpChange(float exp)
    {
        monsterKillCounter++;

        playerobject.GetComponent<CharacterStatHandler>().CurrentStats.exp += exp;

        float currentExp = playerobject.GetComponent<CharacterStatHandler>().CurrentStats.exp;
        float maxExp = playerobject.GetComponent<CharacterStatHandler>().CurrentStats.fullExp;
        if (currentExp >= maxExp)
        {
            playerobject.GetComponent<CharacterStatHandler>().CurrentStats.exp = 0;
            playerobject.GetComponent<CharacterStatHandler>().CurrentStats.lv++;
            playerobject.GetComponent<CharacterStatHandler>().CurrentStats.attackSO.power++;
            playerobject.GetComponent<HealthSystem>().ChangeHealth(5);
            ChangeHpBar(5);

            AudioManager.instance.PlayClip(SFXClipType.LevelUp);
        }
    }
}
