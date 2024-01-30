using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using JetBrains.Annotations;
[Serializable]
public class SaveDataList
{
    public Dictionary<string, List<float>> datas; //��� �����͵�
}
public class SaveRecordData : MonoBehaviour
{
    SaveDataList recordList;
    // Start is called before the first frame update
    void Start()
    {
        recordList = new SaveDataList();
        Dictionary<string,List<float>> dataDic = new Dictionary<string,List<float>>();
        dataDic["�÷��̾�"] = new List<float>() { 0f};
        SaveDataList data = new SaveDataList();
        recordList.datas = dataDic;

        string jsonData = DictionaryJsonUtility.ToJson(dataDic,true);

        string path = Application.dataPath + "/Data";
        Debug.Log("���ϰ�� : " + path);
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
        if (!recordList.datas.ContainsKey(name))//�̸��� ����Ǿ� ���� �ʴٸ�
        {
            recordList.datas.Add(name, new List<float>() { score });//���ھ ù ����.
        }
        else
        {
            recordList.datas[name].Add(score);//�ش��ϴ� �̸��� ����� �߰�.
        }
        SortData(); //���� ���� ����

        string jsonData = DictionaryJsonUtility.ToJson(recordList.datas, true);

        string path = Application.dataPath + "/Data";
        Debug.Log("���ϰ�� : " + path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/RecordData.txt", jsonData);
    }
    public void SortData() //�� �÷��̾� ���� ����� ������������ ����
    {
        foreach(string name in recordList.datas.Keys) 
        {
            recordList.datas[name].Sort(new Comparison<float>((n1, n2) => n2.CompareTo(n1))); ;// ������������ �����Ѵ�.
        }
    }
}
