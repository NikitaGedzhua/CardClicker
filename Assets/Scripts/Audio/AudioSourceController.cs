using UnityEngine;
using UnityEngine.UI;

public class AudioSourceController : MonoBehaviour
{
   [SerializeField] private AudioSource source;
   [SerializeField] private Slider slider;

   private void Start()
   {
      SetSliderValue();
   }

   private void SetSliderValue()
   {
      slider.value = source.volume;
   }
}
