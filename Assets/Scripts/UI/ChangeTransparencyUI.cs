using System.Collections;
using UnityEngine;

public class ChangeTransparencyUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime = 0.4f;
    private bool _faded;
    
    public void FadeChange()
    {
        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, _faded ? 1 : 0));
        _faded = !_faded;
    }
    
    private IEnumerator DoFade(CanvasGroup canGroup, float start, float end)
    {
        float counter = 0f;
        while (counter < fadeTime)
        {
            counter += Time.deltaTime;
            canGroup.alpha = Mathf.Lerp(start, end, counter / fadeTime);

            yield return null;
            if (_faded && canGroup.alpha == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
