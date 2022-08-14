using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
   [SerializeField] private Button exitButton;

   private void Start()
   {
      exitButton.onClick.AddListener(QuitGame);
   }

   private void QuitGame()
   {
      Application.Quit();
   }
}
