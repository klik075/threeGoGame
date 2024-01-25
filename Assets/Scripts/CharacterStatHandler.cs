using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField]
    private CharacterStat CharacterbaseStats;

    public CharacterStat CurrentStats { get; private set; }

    public List<CharacterStat> CstatsModifier = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStats();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCharacterStats()
    {
        CharacterStatSO CharacterSO = null;
        if (CharacterbaseStats.statInfo != null)
        {
            CharacterSO = Instantiate(CharacterbaseStats.statInfo);
        }

        CurrentStats = new CharacterStat { statInfo = CharacterSO };
    }
}
