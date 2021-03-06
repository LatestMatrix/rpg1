﻿using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class CampareMD5ToGenerateVersionNum
{
        private static string _fileName = "VersionNum.xml";

        public static string FileName
        {
                get
                {
                        return _fileName;
                }
        }

        public static void Execute(UnityEditor.BuildTarget target)
        {
                string platform = AssetBundleController.GetPlatformName(target);
                Execute(platform);
                AssetDatabase.Refresh();
        }

        /// <summary>
        /// 对比对应版本目录下的VersionMD5和VersionMD5-old，得到最新的版本号文件VersionNum.xml
        /// </summary>
        /// <param name="platform">目标平台</param>
        public static void Execute(string platform)
        {
                // 读取新旧MD5列表
                string newVersionMD5 = Application.dataPath + '/' + CreateMD5List.ConfigFilePath + '/' + platform + '/' + CreateMD5List.NewFileName;
                string oldVersionMD5 = Application.dataPath + '/' + CreateMD5List.ConfigFilePath + '/' + platform + '/' + CreateMD5List.OldFileName;

                Dictionary<string, string> dicNewMD5Info = ReadMD5File(newVersionMD5);
                Dictionary<string, string> dicOldMD5Info = ReadMD5File(oldVersionMD5);

                // 读取版本号记录文件VersinNum.xml
                string oldVersionNum = Application.dataPath + '/' + CreateMD5List.ConfigFilePath + '/' + platform + '/' +_fileName;
                Dictionary<string, int> dicVersionNumInfo = ReadVersionNumFile(oldVersionNum);

                // 对比新旧MD5信息，并更新版本号，即对比dicNewMD5Info&&dicOldMD5Info来更新dicVersionNumInfo
                foreach (KeyValuePair<string, string> newPair in dicNewMD5Info)
                {
                        // 旧版本中有
                        if (dicOldMD5Info.ContainsKey(newPair.Key))
                        {
                                // MD5一样，则不变
                                // MD5不一样，则+1
                                // 容错：如果新旧MD5都有，但是还没有版本号记录的，则直接添加新纪录，并且将版本号设为1
                                if (dicVersionNumInfo.ContainsKey(newPair.Key) == false)
                                {
                                        dicVersionNumInfo.Add(newPair.Key, 1);
                                }
                                else if (newPair.Value != dicOldMD5Info[newPair.Key])
                                {
                                        int num = dicVersionNumInfo[newPair.Key];
                                        dicVersionNumInfo[newPair.Key] = num + 1;
                                }
                        }
                        else // 旧版本中没有，则添加新纪录，并=1
                        {
                                if (!dicVersionNumInfo.ContainsKey(newPair.Key))
                                        dicVersionNumInfo.Add(newPair.Key, 1);
                        }
                }
                // 不可能出现旧版本中有，而新版本中没有的情况，原因见生成MD5List的处理逻辑

                // 存储最新的VersionNum.xml
                SaveVersionNumFile(dicVersionNumInfo, oldVersionNum);
        }

        /// <summary>
        /// 读取MD5文件
        /// </summary>
        /// <param name="fileName">MD5的XML文件名</param>
        /// <returns>文件名和MD5对应关系的字典</returns>
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

        /// <summary>
        /// 读版本文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>版本文件的词典</returns>
        static Dictionary<string, int> ReadVersionNumFile(string fileName)
        {
                Dictionary<string, int> DicVersionNum = new Dictionary<string, int>();

                // 如果文件不存在，则直接返回
                if (System.IO.File.Exists(fileName) == false)
                        return DicVersionNum;

                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(fileName);
                XmlElement XmlRoot = XmlDoc.DocumentElement;

                foreach (XmlNode node in XmlRoot.ChildNodes)
                {
                        if ((node is XmlElement) == false)
                                continue;

                        string file = (node as XmlElement).GetAttribute("FileName");
                        int num = XmlConvert.ToInt32((node as XmlElement).GetAttribute("Num"));

                        if (DicVersionNum.ContainsKey(file) == false)
                        {
                                DicVersionNum.Add(file, num);
                        }
                }

                XmlRoot = null;
                XmlDoc = null;

                return DicVersionNum;
        }

        /// <summary>
        /// 保存版本文件
        /// </summary>
        /// <param name="data">版本的词典文件</param>
        /// <param name="savePath">保存路径</param>
        static void SaveVersionNumFile(Dictionary<string, int> data, string savePath)
        {
                XmlDocument XmlDoc = new XmlDocument();
                XmlElement XmlRoot = XmlDoc.CreateElement("VersionNum");
                XmlDoc.AppendChild(XmlRoot);

                foreach (KeyValuePair<string, int> pair in data)
                {
                        XmlElement xmlElem = XmlDoc.CreateElement("File");
                        XmlRoot.AppendChild(xmlElem);
                        xmlElem.SetAttribute("FileName", pair.Key);
                        xmlElem.SetAttribute("Num", XmlConvert.ToString(pair.Value));
                }

                XmlDoc.Save(savePath);
                XmlRoot = null;
                XmlDoc = null;
        }

}