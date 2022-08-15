using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadAssetsLocalService : MonoBehaviour
{
    private string _bundlePath = "Assets/AssetBundles";
    private string _bundlename = "cards";
    private string _fileLoadProgress = "LoadFromFile progress: ";
    private string _fileLoadFailed = "Failed to load AssetBundle!";
    private string _assetLoadProgress = "LoadAllAssets progress: ";
    private string _loadingComplete = "Loading completed";
    private string _percent = "%";
    
    private float _progressMultipler = 100f;
    private float _delay = 2f;

    public event Action LoadCompleted; 

    public void StartLoadAssets(Text progressText, Image progressImage)
    {
        StartCoroutine(LoadAssetsAsync(_bundlePath, _bundlename, progressText, progressImage));
    }

    private IEnumerator LoadAssetsAsync(string bundlePath, string bundleName, Text progressText, Image progressImage)
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(bundlePath, bundleName));
        
        while(!bundleLoadRequest.isDone)
        {
            yield return null;
            progressText.text = _fileLoadProgress + (bundleLoadRequest.progress * _progressMultipler) + _percent;
            progressImage.fillAmount = bundleLoadRequest.progress /2;
        }
        yield return bundleLoadRequest;
        
        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            progressText.text = _fileLoadFailed;
            yield break;
        }

        var assetLoadRequest = myLoadedAssetBundle.LoadAllAssetsAsync(typeof(Texture2D));
        while (assetLoadRequest.progress <= 0.9f)
        {
            progressText.text = _assetLoadProgress + (assetLoadRequest.progress * _progressMultipler) + _percent;
        }
        
        assetLoadRequest.completed += operation =>
        {
            var sprites = assetLoadRequest.allAssets.Cast<Texture2D>();
            SaveFilesToLocal(sprites, progressText);
        };

        yield return new WaitForSeconds(_delay);
        progressImage.fillAmount = bundleLoadRequest.progress;
        progressText.text = _loadingComplete;
       
        myLoadedAssetBundle.Unload(false);
        LoadCompleted?.Invoke();
    }

    private void SaveFilesToLocal(IEnumerable<Texture2D> sprites, Text progressText)
    {
        foreach (var card in sprites)
        {
            if (File.Exists(Application.dataPath + card.name + ".png"))
            {
                return;
            }
            File.WriteAllBytes(Application.dataPath + card.name + ".png", card.EncodeToPNG());
            progressText.text = _assetLoadProgress + card.name;
        }
    }
}
