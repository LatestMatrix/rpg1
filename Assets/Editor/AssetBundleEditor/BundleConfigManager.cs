using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using System.Xml;

class BundleConfigManager
{
        #region 常量及变量定义
        private static string _configFileName = "Assets/config/AssetBundle.xml";
        private static string _savePath = "AssetBundle/";
        private static Dictionary<string, List<string>> _bundles = new Dictionary<string,List<string>>();

        public static string SavePath
        {
                get
                {
                        return _savePath;
                }
        }

        public static Dictionary<string, List<string>> Bundles
        {
                get
                {
                        return _bundles;
                }
        }

        public static string ConfigFileName
        {
                get
                {
                        return _configFileName;
                }
        }
        #endregion

        #region 读配置文件
        /// <summary>
        /// 读取配置文件
        /// </summary>
        public static void ReadConfig()
        {
                _bundles.Clear();

                if (!File.Exists(_configFileName))
                {
                        Debug.LogError(_configFileName + "  do not exist!");
                        return;
                }

                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(_configFileName);

                XmlElement xmlRoot = xmlConfig.DocumentElement;

                foreach (XmlNode xmlNode in xmlRoot.ChildNodes)
                {
                        if (xmlNode is XmlElement)
                        {
                                //读取保存路径
                                if (xmlNode.Name.Equals("SavePath"))
                                {
                                        string path = (xmlNode as XmlElement).GetAttribute("Value");

                                        if (Directory.Exists(path))
                                                _savePath = path;
                                        else if (Directory.Exists(Application.dataPath + '/' + path))
                                                _savePath = Application.dataPath + '/' + path;

                                        if (_savePath[_savePath.Length - 1] != '/')
                                                _savePath += '/';
                                }
                                //读取bundle配置文件
                                else if (xmlNode.Name.Equals("Bundle"))
                                {
                                        GetBundle(xmlNode);
                                }
                        }
                }
        }

        /// <summary>
        /// 从文件读取一个Bundle的配置
        /// </summary>
        /// <param name="xmlNode">bundle的xml节点</param>
        private static void GetBundle(XmlNode xmlNode)
        {
                string bundleName = (xmlNode as XmlElement).GetAttribute("Name").Trim();
                if(_bundles.ContainsKey(bundleName))
                {
                        Debug.LogError("More than 1 bundle name " + bundleName);
                        return;
                }

                List<string> files = new List<string>();
                List<string> tmp = new List<string>();

                foreach (XmlNode filePath in xmlNode.ChildNodes)
                {
                        //读取Bundle所需的文件
                        if ((filePath is XmlElement) && filePath.Name.Equals("File"))
                        {
                                string path = (filePath as XmlElement).GetAttribute("Path").Trim();
                                string realPath = Application.dataPath + '/' + path;
                                if (!Directory.Exists(realPath) && !File.Exists(realPath))
                                {
                                        Debug.LogError(path + " do not exist!");
                                        continue;
                                }

                                if (Directory.Exists(realPath) && !realPath[path.Length - 1].Equals('/'))
                                        realPath += '/';

                                if (files.Contains(realPath))
                                        continue;

                                files.Add(realPath);

                                //当前路径包含之前的文件路径，或是之前的路径包含当前文件的路径都去掉
                                tmp.Clear();
                                foreach (string sPath in files)
                                {
                                        if (realPath.Equals(sPath))
                                                continue;
                                        else if (sPath.Contains(realPath))
                                                tmp.Add(sPath);
                                }
                                foreach (string sPath in tmp)
                                {
                                        files.Remove(sPath);
                                }

                        }
                }

                if (files.Count > 0)
                        _bundles.Add(bundleName, files);
        }
        #endregion
}
