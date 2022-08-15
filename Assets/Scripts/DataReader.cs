using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    private string _cardsName = "/CardData.json";
    
    public void LoadFromJson(List<string> lastDataNames)
    {
        if (File.Exists(Application.dataPath + _cardsName))
        {
            var fileContents = File.ReadAllText(Application.dataPath + _cardsName);
            var deserializedData = JsonUtility.FromJson<CardData>(fileContents);
            lastDataNames = deserializedData.names;
        }
    }
}
