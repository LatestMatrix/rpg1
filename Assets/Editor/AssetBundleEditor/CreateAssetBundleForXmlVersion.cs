using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CreateAssetBundleForXmlVersion
{
        private static BuildAssetBundleOptions _option = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle;

        /// <summary>
        /// 版本文件的assetbundle文件打包
        /// </summary>
        /// <param name="target">目标平台</param>
        public static void Execute(UnityEditor.BuildTarget target)
        {
                string assetPath = "Assets/" + CreateMD5List.ConfigFilePath + AssetBundleController.GetPlatformName(target) + '/' + CampareMD5ToGenerateVersionNum.FileName;

                string SavePath = BundleConfigManager.SavePath + AssetBundleController.GetPlatformName(target) + "/VersionNum/";
                if(!Directory.Exists(SavePath))
                {
                        Directory.CreateDirectory(SavePath);
                }
                SavePath += "VersionNum.assetbundle";

                Object obj = AssetDatabase.LoadMainAssetAtPath(assetPath);

                BuildPipeline.BuildAssetBundle(obj, null, SavePath, _option, target);

                AssetDatabase.Refresh();
        }

        static string ConvertToAssetBundleName(string ResName)
        {
                return ResName.Replace('/', '.');
        }
}