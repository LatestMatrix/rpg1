using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CreateAssetBundle
{
        const BuildAssetBundleOptions _option = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle;

        /// <summary>
        /// assetbundle打包
        /// </summary>
        /// <param name="target">目标平台</param>
        public static void Execute(UnityEditor.BuildTarget target)
        {
                BundleConfigManager.ReadConfig();

                Dictionary<string, List<string>> bundles = BundleConfigManager.Bundles;
                string savePath = BundleConfigManager.SavePath + AssetBundleController.GetPlatformName(target) + '/';

                if (!Directory.Exists(savePath))
                        Directory.CreateDirectory(savePath);

                if (bundles.Count <= 0)
                {
                        Debug.LogError(BundleConfigManager.ConfigFileName + " is empty.");
                        return;
                }

                //打包
                foreach(KeyValuePair<string, List<string>> bundle in bundles)
                {
                        List<Object> objs = new List<Object>();
                        foreach(string path in bundle.Value)
                        {
                                FillObjToList(path, objs);
                        }
                        BuildPipeline.BuildAssetBundle(objs[0], objs.ToArray(), savePath + bundle.Key + ".assetbundle", _option, target);
                }

                //计算MD5
                CreateMD5List.Execute(target);

                //比较MD5
                CampareMD5ToGenerateVersionNum.Execute(target);

                //修改Version文件
                CreateAssetBundleForXmlVersion.Execute(target);

                // scene目录下的资源
                AssetDatabase.Refresh();
        }

        /// <summary>
        /// 实际路径转为项目的相对路径
        /// </summary>
        /// <param name="path">实际路径</param>
        /// <returns>项目的相对路径</returns>
        static string ConvertToAssetPath(string path)
        {
                string assetPath = path.Replace('\\', '/');
                assetPath = assetPath.Replace(Application.dataPath + '/', "");
                return "Assets/" + assetPath;
        }

        /// <summary>
        /// 根据路径获取打包对象
        /// </summary>
        /// <param name="path">文件路径，文件夹或文件</param>
        /// <param name="objs">对象列表</param>
        static void FillObjToList(string path, List<Object> objs)
        {
                if(Directory.Exists(path))
                {
                        string[] filePaths = Directory.GetFiles(path);
                        foreach (string filePath in filePaths)
                        {
                                if(!filePath.Contains(".meta"))
                                {
                                        string assetPath = ConvertToAssetPath(filePath);
                                        Object obj = AssetDatabase.LoadMainAssetAtPath(assetPath);
                                        objs.Add(obj);
                                }
                        }
                }
                else if(File.Exists(path))
                {
                        if (!path.Contains(".meta"))
                        {
                                string assetPath = ConvertToAssetPath(path);
                                Object obj = AssetDatabase.LoadMainAssetAtPath(assetPath);
                                objs.Add(obj);
                        }
                }
        }
}