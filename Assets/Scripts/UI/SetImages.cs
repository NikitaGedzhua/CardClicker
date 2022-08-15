using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SetImages : MonoBehaviour
{
    [SerializeField] private ButtonActionsController buttonsController;
    [SerializeField] private Image[] headerImages;
    [SerializeField] private string[] cardNames =  { "Card1", "Card2", "Card3", "Card4", "Card5" };

    private List<Sprite> _images = new List<Sprite>();
    
    private void Start()
    {
        LoadImages();
        SetHeaderImages();
        SetCardsImage();
    }

    private void SetHeaderImages()
    {
        for (var i = 0; i < headerImages.Length; i++)
        {
            var image = headerImages[i];
            image.sprite = _images[i];
        }
    }
    
    private void SetCardsImage()
    {
        for (int i = 0; i < buttonsController.ButtonsInView.Count; i++)
        {
            var button = buttonsController.ButtonsInView[i];
            button.image.sprite = _images[Random.Range(0,4)];
        }
    }

    private void LoadImages()
    {
        foreach (var card in cardNames)
        {
            if (!File.Exists(Application.dataPath  + card + ".png")) return;
            
            var fileData = File.ReadAllBytes(Application.dataPath + card + ".png");
            
            Texture2D tex = new Texture2D (2, 2, TextureFormat.BGRA32, false);
            tex.LoadImage(fileData);
            
            var sprite =  TextureToSprite(tex);
            _images.Add(sprite);
        }
    }

    private Sprite TextureToSprite(Texture2D texture) => 
        Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 50f, 0, SpriteMeshType.FullRect);
    
    private Texture2D DuplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
            source.width,
            source.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear);
 
        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}
