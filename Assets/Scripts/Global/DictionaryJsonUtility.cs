using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataDictionary
{
    public string name; //플레이어 이름
    public List<float> records = new List<float>(); //기록들 List
}

[Serializable]
public class JsonDataArray
{
    public List<DataDictionary> data;
}

public static class DictionaryJsonUtility
{
    public static string ToJson(Dictionary<string,List<float>> jsonDicData, bool pretty = false)
    {//딕셔너리를 Json으로 파싱한다.
        List<DataDictionary> dataList = new List<DataDictionary>();//임시 딕셔너리 리스트
        DataDictionary dictionaryData;//임시 딕셔너리 데이터
        foreach (string key in jsonDicData.Keys)
        {
            dictionaryData = new DataDictionary();
            dictionaryData.name = key; //임시 딕셔너리의 이름을 플레이어 이름으로 설정
            foreach (float record in jsonDicData[key])
                dictionaryData.records.Add(record); //플레이어의 기록들을 저장.
            dataList.Add(dictionaryData); //딕셔너리 리스트에 딕셔너리 데이터 저장.
        }
        JsonDataArray arrayJson = new JsonDataArray();
        arrayJson.data = dataList; //하나의 클래스로 묶고 안에 리스트값 복사

        return JsonUtility.ToJson(arrayJson, pretty);
    }
    public static Dictionary<string, List<float>> FromJson(string jsonData)
    {//Json을 딕셔너리로 파싱한다.
        List<DataDictionary> dataList = JsonUtility.FromJson<List<DataDictionary>>(jsonData);

        Dictionary<string, List<float>> returnDictionary = new Dictionary<string, List<float>>();

        for (int i = 0; i < dataList.Count; i++)
        {
            DataDictionary dictionaryData = dataList[i];//받아온 딕셔너리 리스트를 정의한 딕셔너리 데이터로 옮겨온다.
            returnDictionary[dictionaryData.name] = dictionaryData.records;//사전에 key와 value를 저장한다.
        }

        return returnDictionary;
    }
}