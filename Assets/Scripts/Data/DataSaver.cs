using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataSaver : MonoBehaviour
{
    [SerializeField] private CardData cardData = new CardData();
    private string _cardsName = "/CardData.json";
    
    public void SaveIntoJson(List<Button> buttonList)
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            name = buttonList[i].GetComponentsInChildren<Image>()[1].sprite.name;
            cardData.names.Add(name);
        }
        
        var data = JsonUtility.ToJson(cardData);
        File.WriteAllText(Application.dataPath + _cardsName, data);
        Debug.Log("Data saved");
    }
}
