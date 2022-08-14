using UnityEngine;
using UnityEngine.UI;

public class ButtonAudioEffect : MonoBehaviour
{ 
   [SerializeField] private Button[] buttons;
   [SerializeField] private AudioSource soundSource;
  
   private void Start() 
   {
     SetButtonsToPlay();
   }

   private void SetButtonsToPlay()
   {
     foreach (var button in buttons)
     {
       button.onClick.AddListener(PlayAudioEffect);
     }
   }
  
   private void PlayAudioEffect()
   {
     soundSource.Play();
   }
}
