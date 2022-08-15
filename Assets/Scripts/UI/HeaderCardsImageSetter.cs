using UnityEngine;
using UnityEngine.UI;

public class HeaderCardsImageSetter : MonoBehaviour
{
    [SerializeField] private Image[] headerImages;
    [SerializeField] private LoadImages loadImages;

    private void Start()
    {
        SetHeaderImages();
    }

    private void SetHeaderImages()
    {
        for (var i = 0; i < headerImages.Length; i++)
        {
            var image = headerImages[i];
            image.sprite = loadImages.Images[i];
        }
    }
}
