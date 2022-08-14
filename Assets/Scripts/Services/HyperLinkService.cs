using UnityEngine;
using UnityEngine.UI;

public class HyperLinkService : MonoBehaviour
{
   [SerializeField] private string gitLink = "https://github.com/NikitaGedzhua/Genesis";
   [SerializeField] private Button gitLinkButton;

   private void Start()
   {
      gitLinkButton.onClick.AddListener(() => OpenUrl(gitLink));
   }

   public void OpenUrl(string url)
   {
      Application.OpenURL(url);
   }
}
