using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
    [SerializeField] GameObject playerobject;
    [SerializeField] GameObject enemyprefab;
    
    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;

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
            GameObject enemyInstance = Instantiate(enemyprefab);
            float x = Random.Range(0f, 0f);
            float y = 0f;
            enemyInstance.transform.position = new Vector3(x, y, 0);
        }
        else if(playerobject.GetComponent<CharacterStatHandler>().CurrentStats.lv == 2)
        {
            Instantiate(enemyprefab);
        }
        else
        {
            Instantiate(enemyprefab);
        }

    }
}
