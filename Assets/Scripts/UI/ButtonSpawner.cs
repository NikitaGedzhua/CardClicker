using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ButtonSpawner : MonoBehaviour
{
    [SerializeField] private ButtonActionsController actionsController;
    [SerializeField] private LoadImages loadImages;
    [SerializeField] private DataSaver dataSaver;
    [SerializeField] private Button butonPrefab;
    [SerializeField] private Button resetButton;
    [SerializeField] private int buttonsAmount = 5;
    
    private List<Button> buttonsInView = new List<Button>();
    private List<Sprite> imagesSpawnedCards = new List<Sprite>();
    private List<string> _lastDataNames = new List<string>();
    
    private void Start()
    {
        LoadFromJson();
        SpawnCards();
        AddActionsToCards();
        SaveSpawnedCardsImage();
        resetButton.onClick.AddListener(ResetCardWindow);
        SetResetButtonPosition();
    }
    
    private void OnApplicationQuit()
    {
        dataSaver.SaveIntoJson(buttonsInView);
    }

    private void SpawnCards()
    {
        if (_lastDataNames.Count > 0)
        {
            for (int i = 0; i < _lastDataNames.Count; i++)
            {
                var button = Instantiate(butonPrefab, actionsController.gameObject.transform);
                var image = _lastDataNames[i];
                buttonsInView.Add(button);
                button.GetComponentsInChildren<Image>()[1].sprite = loadImages.GetImage(image);
            }
        }
        else
        {
            for (int i = 0; i < buttonsAmount; i++)
            {
                var button = Instantiate(butonPrefab, actionsController.gameObject.transform);
                buttonsInView.Add(button);
                button.GetComponentsInChildren<Image>()[1].sprite = loadImages.Images[Random.Range(0,4)];
            }
        }
    }

    private void SaveSpawnedCardsImage()
    {
        for (var i = 0; i < buttonsInView.Count; i++)
        {
            var sprite = buttonsInView[i].GetComponentsInChildren<Image>()[1].sprite;
            imagesSpawnedCards.Add(sprite);
        }
    }
    
    private void AddActionsToCards()
    {
        actionsController.AddToListAllButtons(buttonsInView);
    }

    private void SetResetButtonPosition()
    {
        resetButton.transform.parent.SetAsLastSibling();
    }

    private void ResetCardWindow()
    {
        for (var i = 0; i < buttonsInView.Count; i++)
        {
            var button = buttonsInView[i];
            Destroy(button.gameObject);
        }
        buttonsInView.Clear();
        RespawnCards();
        AddActionsToCards();
        SetResetButtonPosition();
    }

    private void RespawnCards()
    {
        for (int i = 0; i < buttonsAmount; i++)
        {
            var button = Instantiate(butonPrefab, actionsController.gameObject.transform);
            buttonsInView.Add(button);
            button.GetComponentsInChildren<Image>()[1].sprite = i >= imagesSpawnedCards.Count ? loadImages.Images[Random.Range(0, 4)] : imagesSpawnedCards[i];
        }
    }

    private void LoadFromJson()
    {
        if (File.Exists(Application.dataPath + "/CardData.json"))
        {
            var fileContents = File.ReadAllText(Application.dataPath + "/CardData.json");
            var deserializedData = JsonUtility.FromJson<CardData>(fileContents);
            _lastDataNames = deserializedData.names;
        }
    }
}
