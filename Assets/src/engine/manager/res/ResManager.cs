
using engine.core;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;

namespace engine.manager
{
    public class ResManager
    {
        private string configFile = "/config/config.sqlite";

        public ResManager()
        {
#if UNITY_EDITOR
            Log.UILog("pc");
            LoadConfigFile(Application.dataPath + "/Plugins/Android/assets/" + configFile);
#else
            Log.UILog("mobile");
            CopyFile();
            LoadConfigFile(Application.persistentDataPath + configFile);
#endif
            LoadUI();
        }

        private void CopyFile()
        {
            Log.UILog("copy file");
            string filepath = Application.persistentDataPath + configFile;
            if (!File.Exists(filepath))
            {
                Log.UILog("copy from " + Application.streamingAssetsPath + configFile);
                Log.UILog("file exist? " + File.Exists(Application.streamingAssetsPath + configFile).ToString());
                Log.UILog(Application.dataPath);
                try
                {
                    WWW loadDB = new WWW(Application.streamingAssetsPath + configFile);
                    Log.UILog(loadDB.error);
                    while (!loadDB.isDone)
                    {
                    }
                    File.WriteAllBytes(filepath, loadDB.bytes);
                }
                catch (System.Exception e) 
                {
                    Log.UILog(e.Message);
                }
                
                Log.UILog("copy to " + Application.persistentDataPath + configFile);
            }
        }

        private void LoadConfigFile(string file)
        {
            DB db = new DB(@"Data Source=" + file);
            SqliteDataReader reader = db.ReadFullTable("config");
            while (reader.Read())
            {
                Log.UILog(reader.GetString(0));
                Log.UILog(reader.GetString(1));
            }
            //db.ChangePassword("");
            reader.Close();
            db.CloseSqlConnection();
        }

        private void LoadUI()
        {
            GameObject obj = Resources.Load<GameObject>("ui/joystick");
            NGUITools.AddChild(NGUITools.FindCameraForLayer((obj).layer).gameObject, obj).name = "joystick";
            obj = Resources.Load<GameObject>("ui/skillArea");
            NGUITools.AddChild(NGUITools.FindCameraForLayer((obj).layer).gameObject, obj);
        }

        public GameObject Load(string name)
        {
            return Resources.Load<GameObject>(name);
        }

    }
}
