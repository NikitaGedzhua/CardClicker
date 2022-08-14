using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTransparencyUI : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private Text[] texts;
    [SerializeField] private float fadeTime = 1f;
     private Color _newColorImage;
     private Color _newColorText;

    public void ChangeTransparency()
    {
       SetTextsTransparency();
       SetImagesTransparency(); 
    }

     private void SetTextsTransparency()
     {
         foreach (var text in texts)
         {
             StartCoroutine(FadeOutText(text));
         }
     }
     
     private void SetImagesTransparency()
     {
         foreach (var image in images)
         {
             StartCoroutine(FadeOutImage(image));
         }
     }

    private IEnumerator FadeOutText(Text text)
    {
        _newColorText = text.color;
        _newColorText.a = 0;
        while (text.color.a > 0)
        {
            text.color = Color.Lerp(text.color, _newColorText, fadeTime * Time.deltaTime);
            yield return null; 
            
        }
        text.gameObject.SetActive(false);
    }
    
    private IEnumerator FadeOutImage(Image image)
    {
        _newColorImage = image.color;
        _newColorImage.a = 0;
        while (image.color.a > 0)
        {
            if (image.color.a <= 0.1f) image.color = _newColorImage;
            
            image.color = Color.Lerp(image.color, _newColorImage, fadeTime * Time.deltaTime);
            yield return null; 
        }
        image.gameObject.SetActive(false);
    }
}
