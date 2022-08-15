using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Button button;
    [SerializeField] private float particleTime = 1f;
    [SerializeField] private float fadeTime = 0.5f;
    
    private void Start()
    {
        button.onClick.AddListener(() => ShowParticle(button));
        button.onClick.AddListener(() => FadeOutButton(button));
    }

    private void ShowParticle(Button bt)
    {
        var shape = particle.shape;
        shape.sprite = bt.image.sprite;
        shape.texture = bt.image.sprite.texture;
      
        var newParticle =  Instantiate(particle, bt.transform.position, bt.transform.rotation);
      
        DestroyGameObject(newParticle.gameObject, particleTime);
    }
    
    private void FadeOutButton(Button button)
    {
        button.image.DOFade(0, fadeTime);
    }
    
    private void DestroyGameObject(GameObject button, float time)
    {
        Destroy(button, time);
    }
}
