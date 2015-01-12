using UnityEngine;
using UnityEditor;
using System.Collections;

public class ExportAssetBundles
{

    [MenuItem("Export/Build AssetBundle From Selection")]
    static void ExportResourceWindows()
    {
        string path = EditorUtility.SaveFilePanel("save resource", "", "new resource", "assetbundle");
        if (!string.IsNullOrEmpty(path))
        {
            Object[] selections = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selections, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows64);
            Selection.objects = selections;
        }
    }

    [MenuItem("Export/Build AssetBundle From Selection - no Track")]
    static void ExportResourceNoTrack()
    {

        string path = EditorUtility.SaveFilePanel("save resource", "", "new resource", "assetbundle");
        if (!string.IsNullOrEmpty(path))
        {
            BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path);
        }
    }
}
