using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public class CreateMD5List
{
        private static string _configFilePath = "config/VersionNum/";
        private static string _oldFileName = "VersionMD5-old.xml";
        private static string _newFileName = "VersionMD5.xml";

        public static string ConfigFilePath
        {
                get
                {
                        return _configFilePath;
                }
        }

        public static string OldFileName
        {
                get
                {
                        return _oldFileName;
                }
        }

        public static string NewFileName
        {
                get
                {
                        return _newFileName;
                }
        }

        public static void Execute(UnityEditor.BuildTarget target)
        {
                string platform = AssetBundleController.GetPlatformName(target);
                Execute(platform);
                AssetDatabase.Refresh();
        }

        /// <summary>
        /// 计算assetbunddle的MD5保存到XML文件中
        /// </summary>
        /// <param name="platform">目标平台</param>
        public static void Execute(string platform)
        {
                Dictionary<string, string> DicFileMD5 = new Dictionary<string, string>();
                MD5CryptoServiceProvider md5Generator = new MD5CryptoServiceProvider();

                string dir = BundleConfigManager.SavePath + platform;
                foreach (string filePath in Directory.GetFiles(dir))
                {
                        if (filePath.Contains(".meta") || filePath.Contains("VersionMD5") || filePath.Contains(".xml"))
                                continue;

                        FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        byte[] hash = md5Generator.ComputeHash(file);
                        string strMD5 = System.BitConverter.ToString(hash);
                        file.Close();

                        string key = filePath.Substring(dir.Length + 1, filePath.Length - dir.Length - 1);

                        if (DicFileMD5.ContainsKey(key) == false)
                                DicFileMD5.Add(key, strMD5);
                        else
                                Debug.LogWarning("<Two File has the same name> name = " + filePath);
                }

                string savePath = Application.dataPath + '/' + _configFilePath + platform + '/';
                if (Directory.Exists(savePath) == false)
                        Directory.CreateDirectory(savePath);

                // 删除前一版的old数据
                if (File.Exists(savePath + _oldFileName))
                {
                        System.IO.File.Delete(savePath + _oldFileName);
                }

                // 如果之前的版本存在，则将其名字改为VersionMD5-old.xml
                if (File.Exists(savePath + _newFileName))
                {
                        System.IO.File.Move(savePath + _newFileName, savePath + _oldFileName);
                }

                XmlDocument XmlDoc = new XmlDocument();
                XmlElement XmlRoot = XmlDoc.CreateElement("Files");
                XmlDoc.AppendChild(XmlRoot);
                foreach (KeyValuePair<string, string> pair in DicFileMD5)
                {
                        XmlElement xmlElem = XmlDoc.CreateElement("File");
                        XmlRoot.AppendChild(xmlElem);

                        xmlElem.SetAttribute("FileName", pair.Key);
                        xmlElem.SetAttribute("MD5", pair.Value);
                }

                // 读取旧版本的MD5
                Dictionary<string, string> dicOldMD5 = ReadMD5File(savePath + _oldFileName);
                // VersionMD5-old中有，而VersionMD5中没有的信息，手动添加到VersionMD5
                foreach (KeyValuePair<string, string> pair in dicOldMD5)
                {
                        if (DicFileMD5.ContainsKey(pair.Key) == false)
                                DicFileMD5.Add(pair.Key, pair.Value);
                }

                XmlDoc.Save(savePath + _newFileName);
                XmlDoc = null;
        }

        /// <summary>
        /// 读取MD5的XML并返回一个词典
        /// </summary>
        /// <param name="fileName">MD5文件名</param>
        /// <returns>文件名和MD5对应的词典</returns>
        static Dictionary<string, string> ReadMD5File(string fileName)
        {
                Dictionary<string, string> DicMD5 = new Dictionary<string, string>();

                // 如果文件不存在，则直接返回
                if (System.IO.File.Exists(fileName) == false)
                        return DicMD5;

                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(fileName);
                XmlElement XmlRoot = XmlDoc.DocumentElement;

                foreach (XmlNode node in XmlRoot.ChildNodes)
                {
                        if ((node is XmlElement) == false)
                                continue;

                        string file = (node as XmlElement).GetAttribute("FileName");
                        string md5 = (node as XmlElement).GetAttribute("MD5");

                        if (DicMD5.ContainsKey(file) == false)
                        {
                                DicMD5.Add(file, md5);
                        }
                }

                XmlRoot = null;
                XmlDoc = null;

                return DicMD5;
        }

}