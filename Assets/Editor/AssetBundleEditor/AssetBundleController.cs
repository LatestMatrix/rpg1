using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class AssetBundleController : EditorWindow
{
        [MenuItem("AssetBundleEditor/AssetBundle For Windows32", false, 1)]
        public static void ExecuteWindows32()
        {
                CreateAssetBundle.Execute(UnityEditor.BuildTarget.StandaloneWindows);
                EditorUtility.DisplayDialog("AssetbundleEditor", "Assetbundle package completed!", "OK");
        }

        [MenuItem("AssetBundleEditor/AssetBundle For IPhone", false, 2)]
        public static void ExecuteIPhone()
        {
                CreateAssetBundle.Execute(UnityEditor.BuildTarget.iPhone);
                EditorUtility.DisplayDialog("AssetbundleEditor", "Assetbundle package completed!", "OK");
        }

        [MenuItem("AssetBundleEditor/AssetBundle For Mac", false, 3)]
        public static void ExecuteMac()
        {
                CreateAssetBundle.Execute(UnityEditor.BuildTarget.StandaloneOSXIntel64);
                EditorUtility.DisplayDialog("AssetbundleEditor", "Assetbundle package completed!", "OK");
        }

        [MenuItem("AssetBundleEditor/AssetBundle For Android", false, 4)]
        public static void ExecuteAndroid()
        {
                CreateAssetBundle.Execute(UnityEditor.BuildTarget.Android);
                EditorUtility.DisplayDialog("AssetbundleEditor", "Assetbundle package completed!", "OK");
        }

        [MenuItem("AssetBundleEditor/AssetBundle For WebPlayer", false, 5)]
        public static void ExecuteWebPlayer()
        {
                CreateAssetBundle.Execute(UnityEditor.BuildTarget.WebPlayer);
                EditorUtility.DisplayDialog("AssetbundleEditor", "Assetbundle package completed!", "OK");
        }

        void OnGUI()
        {
        }

        public static string GetPlatformName(UnityEditor.BuildTarget target)
        {
                string platform = "Windows32";
                switch (target)
                {
                        case BuildTarget.StandaloneWindows:
                                platform = "Windows32";
                                break;
                        case BuildTarget.StandaloneWindows64:
                                platform = "Windows64";
                                break;
                        case BuildTarget.iPhone:
                                platform = "IOS";
                                break;
                        case BuildTarget.StandaloneOSXUniversal:
                                platform = "Mac";
                                break;
                        case BuildTarget.Android:
                                platform = "Android";
                                break;
                        case BuildTarget.WebPlayer:
                                platform = "WebPlayer";
                                break;
                        default:
                                break;
                }
                return platform;
        }
}