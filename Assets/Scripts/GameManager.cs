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
        InvokeRepeating("makeEnemy", 0.0f, 1.0f);

    }

    void makeEnemy()
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
}
