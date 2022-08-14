using UnityEditor;

public class CreateAssetBundles
{
   private static string _path = "Assets/AssetBundles";
   
   [MenuItem("Assets/Build Asset Bundles")]
   static void BuildAssetBundles()
   {
      BuildPipeline.BuildAssetBundles(_path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
   }
}
