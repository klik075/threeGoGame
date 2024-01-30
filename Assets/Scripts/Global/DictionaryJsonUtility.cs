using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataDictionary
{
    public string name; //�÷��̾� �̸�
    public List<float> records = new List<float>(); //��ϵ� List
}

[Serializable]
public class JsonDataArray
{
    public List<DataDictionary> data;
}

public static class DictionaryJsonUtility
{
    public static string ToJson(Dictionary<string,List<float>> jsonDicData, bool pretty = false)
    {//��ųʸ��� Json���� �Ľ��Ѵ�.
        List<DataDictionary> dataList = new List<DataDictionary>();//�ӽ� ��ųʸ� ����Ʈ
        DataDictionary dictionaryData;//�ӽ� ��ųʸ� ������
        foreach (string key in jsonDicData.Keys)
        {
            dictionaryData = new DataDictionary();
            dictionaryData.name = key; //�ӽ� ��ųʸ��� �̸��� �÷��̾� �̸����� ����
            foreach (float record in jsonDicData[key])
                dictionaryData.records.Add(record); //�÷��̾��� ��ϵ��� ����.
            dataList.Add(dictionaryData); //��ųʸ� ����Ʈ�� ��ųʸ� ������ ����.
        }
        JsonDataArray arrayJson = new JsonDataArray();
        arrayJson.data = dataList; //�ϳ��� Ŭ������ ���� �ȿ� ����Ʈ�� ����

        return JsonUtility.ToJson(arrayJson, pretty);
    }
    public static Dictionary<string, List<float>> FromJson(string jsonData)
    {//Json�� ��ųʸ��� �Ľ��Ѵ�.
        List<DataDictionary> dataList = JsonUtility.FromJson<List<DataDictionary>>(jsonData);

        Dictionary<string, List<float>> returnDictionary = new Dictionary<string, List<float>>();

        for (int i = 0; i < dataList.Count; i++)
        {
            DataDictionary dictionaryData = dataList[i];//�޾ƿ� ��ųʸ� ����Ʈ�� ������ ��ųʸ� �����ͷ� �Űܿ´�.
            returnDictionary[dictionaryData.name] = dictionaryData.records;//������ key�� value�� �����Ѵ�.
        }

        return returnDictionary;
    }
}