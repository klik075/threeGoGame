using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform Player {get; private set;}
    
    private void Awake()
    {
        instance = this;
        // Player = GameObject.FindGameObjectsWithTag(playerTag).transform;
    }
}
