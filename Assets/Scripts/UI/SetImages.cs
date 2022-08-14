using UnityEngine;
using UnityEngine.UI;

public class SetImages : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private string[] cardNames =  { "Card1", "Card2", "Card3", "Card4", "Card5" };
    private Sprite _sprite;
    
    private void Awake()
    {
        ChangeImage();
    }

    private void ChangeImage()
    {
         var card = Resources.Load<Texture2D>(cardNames[0]);
         _sprite = TextureToSprite(card);
         buttons[0].image.sprite = _sprite;
    }

    private Sprite TextureToSprite(Texture2D texture) => 
        Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 50f, 0, SpriteMeshType.FullRect);
}
