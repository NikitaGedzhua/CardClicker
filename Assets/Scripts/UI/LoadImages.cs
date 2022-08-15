using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadImages : MonoBehaviour
{
    private string[] cardNames =  { "Card1", "Card2", "Card3", "Card4", "Card5" };
    private List<Sprite> _images = new List<Sprite>();

    public List<Sprite> Images => _images;
    
    private void Awake()
    {
        StartLoadImages();
    }

    private void StartLoadImages()
    {
        foreach (var card in cardNames)
        {
            if (!File.Exists(Application.dataPath  + card + ".png")) return;
            
            var fileData = File.ReadAllBytes(Application.dataPath + card + ".png");
            
            Texture2D tex = new Texture2D (2, 2, TextureFormat.BGRA32, false);
            tex.LoadImage(fileData);
            
            var sprite = TextureToSprite(tex);
            sprite.name = card;
            _images.Add(sprite);
        }
    }

    public Sprite GetImage(string imageName)
    {
        return _images.Find(i => i.name == imageName);
    }

    private Sprite TextureToSprite(Texture2D texture) => 
        Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), 
            new Vector2(0.5f, 0.5f), 50f, 0, SpriteMeshType.FullRect);
}
