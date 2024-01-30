using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using JetBrains.Annotations;
[Serializable]
public class SaveDataList
{
    public Dictionary<string, List<float>> datas; //기록 데이터들
}
public class SaveRecordData : MonoBehaviour
{
    SaveDataList recordList;
    // Start is called before the first frame update
    void Start()
    {
        recordList = new SaveDataList();
        Dictionary<string,List<float>> dataDic = new Dictionary<string,List<float>>();
        dataDic["플레이어"] = new List<float>() { 0f};
        SaveDataList data = new SaveDataList();
        recordList.datas = dataDic;

        string jsonData = DictionaryJsonUtility.ToJson(dataDic,true);

        string path = Application.dataPath + "/Data";
        Debug.Log("파일경로 : " + path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/RecordData.txt", jsonData);

        string fromJsonData = File.ReadAllText(path + "/RecordData.txt");

        SaveDataList recordFromData = new SaveDataList();
        recordFromData.datas = DictionaryJsonUtility.FromJson(fromJsonData);

        recordList = recordFromData;
    }
    public void SaveCurrentData(string name,float score)
    {
        if (!recordList.datas.ContainsKey(name))//이름이 저장되어 있지 않다면
        {
            recordList.datas.Add(name, new List<float>() { score });//스코어를 첫 저장.
        }
        else
        {
            recordList.datas[name].Add(score);//해당하는 이름에 기록을 추가.
        }
        SortData(); //오름 차순 정렬

        string jsonData = DictionaryJsonUtility.ToJson(recordList.datas, true);

        string path = Application.dataPath + "/Data";
        Debug.Log("파일경로 : " + path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/RecordData.txt", jsonData);
    }
    public void SortData() //각 플레이어 마다 기록을 오름차순으로 정렬
    {
        foreach(string name in recordList.datas.Keys) 
        {
            recordList.datas[name].Sort(new Comparison<float>((n1, n2) => n2.CompareTo(n1))); ;// 오름차순으로 정렬한다.
        }
    }
}
