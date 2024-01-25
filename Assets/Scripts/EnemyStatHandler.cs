using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    [SerializeField]
    private EnemyStat EnemybaseStats;

    public EnemyStat CurrentStats { get; private set; }

    public List<EnemyStat> statsModifier = new List<EnemyStat>();

    private void Awake()
    {
        UpdateEnemyStats();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateEnemyStats()
    {
        EnemyStatSO EnemySO = null;
        if (EnemybaseStats.statInfo != null)
        {
            EnemySO = Instantiate(EnemybaseStats.statInfo);
        }

        CurrentStats = new EnemyStat {statInfo = EnemySO };


    }
}
