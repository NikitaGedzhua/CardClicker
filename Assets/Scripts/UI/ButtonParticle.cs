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
    }

    private void ShowParticle(Button bt)
    {
        var shape = particle.shape;
        shape.sprite = bt.GetComponentsInChildren<Image>()[1].sprite;
        shape.texture = bt.GetComponentsInChildren<Image>()[1].sprite.texture;
      
        var newParticle =  Instantiate(particle, bt.GetComponentsInChildren<Image>()[1].transform.position, 
            bt.GetComponentsInChildren<Image>()[1].transform.rotation);
        DestroyGameObject(newParticle.gameObject, particleTime);
        FadeOutButton(button);
    }
    
    private void FadeOutButton(Button but)
    {
        but.image.DOFade(0, fadeTime);
        but.GetComponentsInChildren<Image>()[1].DOFade(0, fadeTime);
    }
    
    private void DestroyGameObject(GameObject button, float time)
    {
        Destroy(button, time);
    }
}
