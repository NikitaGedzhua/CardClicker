using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadBundleManager : MonoBehaviour
{
    [SerializeField] private LoadAssetsLocalService loadAssetsBundleService;
    [SerializeField] private Text progressLoadText;
    [SerializeField] private Image progressLoadImage;

    private void Awake()
    {
        loadAssetsBundleService.LoadCompleted += LoadMainScene;
    }

    private void OnDisable()
    {
        loadAssetsBundleService.LoadCompleted -= LoadMainScene;
    }

    private void Start()
    {
       LoadAssetBundles();
    }

    private void LoadAssetBundles()
    {
        loadAssetsBundleService.StartLoadAssets(progressLoadText, progressLoadImage);
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
