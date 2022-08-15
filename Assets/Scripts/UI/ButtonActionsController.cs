using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActionsController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float timeToDestroy = 1f;
    [SerializeField] private float timeToMove = 0.6f;

    public void AddToListAllButtons(List<Button> buttonList)
    {
       foreach (var button in buttonList)
       {
          button.onClick.AddListener(() => ButtonActions(buttonList, button));
       }
    }

    private void ButtonActions(List<Button> buttonList, Button button) 
    { 
       SetButtonNewPosition(buttonList, button); 
       RemoveButtonFromList(buttonList, button); 
       DestroyGameObject(button.gameObject, timeToDestroy); 
    }
    
    private void SetButtonNewPosition(List<Button> buttonList, Button button)
    {
       var currentIndex = buttonList.IndexOf(button);
     
       if (currentIndex - 1 < -1 || currentIndex + 1 >= buttonList.Count) return;
      
       var indexNextButton = currentIndex + 1;
     
       for (var i = currentIndex ; i < indexNextButton; i++)
       {
          if (indexNextButton >= buttonList.Count) return;
          canvasGroup.interactable = false;
          indexNextButton++;
          var prevButton = buttonList[i]; 
          var nextButton = i + 1;
          
          buttonList[nextButton].transform.DOLocalMove(prevButton.gameObject.transform.localPosition, timeToMove)
             .SetEase(Ease.InQuad).onComplete += () => canvasGroup.interactable = true;
       }
    }

    private void DestroyGameObject(GameObject button, float time)
   {
      Destroy(button, time);
   }

   private void RemoveButtonFromList(List<Button> buttonList, Button button)
   {
      buttonList.Remove(button); 
   }
}
