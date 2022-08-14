using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle; 
    [SerializeField] private GameObject scrollContent; 
    private readonly List<Button> _buttonsInView = new List<Button>();
    private float _timeToDestroy = 1f;
    private float _timeToMove = 1f;
    
    private void Start() 
    {
      AddToListAllButtons(); 
    }
    
    private void AddToListAllButtons() 
    { 
       for (int i = 0; i < scrollContent.transform.childCount; i++) 
       { 
          var button = scrollContent.transform.GetChild(i).GetComponent<Button>(); 
          _buttonsInView.Add(button); 
          button.onClick.AddListener((() =>ButtonActions(button) )); 
       } 
    }

    private void ButtonActions(Button button) 
    { 
       SetButtonNewPosition(button); 
       ShowPurticle(button); 
       FadeOutButton(button); 
       RemoveButtonFromList(button); 
       DestroyGameObject(button.gameObject, _timeToDestroy); 
    }
    
    private void SetButtonNewPosition(Button button)
    {
       var currentIndex = _buttonsInView.IndexOf(button);
     
       if (currentIndex - 1 < -1 || currentIndex + 1 >= _buttonsInView.Count) return;
      
       var indexNextButton = currentIndex + 1;
     
       for (var i = currentIndex ; i < indexNextButton; i++)
       {
          if (indexNextButton >= _buttonsInView.Count) return;
          
          indexNextButton++;
          var prevButton = _buttonsInView[i]; 
          var nextButton = i + 1;
          
          _buttonsInView[nextButton].transform.DOLocalMove(prevButton.gameObject.transform.localPosition, _timeToMove)
             .SetEase(Ease.InQuad);
       }
    }
   private void ShowPurticle(Button button)
   {
      var shape = particle.shape;
      shape.sprite = button.image.sprite;
      shape.texture = button.image.sprite.texture;
      
      var newParticle =  Instantiate(particle, button.transform.position, button.transform.rotation);
      
      DestroyGameObject(newParticle.gameObject, _timeToDestroy);
   }

   private void FadeOutButton(Button button)
   {
      button.image.DOFade(0, _timeToDestroy / 2);
   }

   private void DestroyGameObject(GameObject button, float time)
   {
      Destroy(button, time);
   }

   private void RemoveButtonFromList(Button button)
   {
      _buttonsInView.Remove(button); 
   }
}
