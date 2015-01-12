
using engine.core;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;

namespace engine.manager
{
    public class ResManager
    {
        private string configDir = "/config";
        private string configFile = "/config/config.sqlite";

        public ResManager()
        {
#if UNITY_EDITOR
            LoadConfigFile(Application.dataPath + "/StreamingAssets" + configFile);
#else
            CopyFile();
            LoadConfigFile(Application.persistentDataPath + configFile);
#endif
            LoadUI();
        }

        private void CopyFile()
        {
            string fromFilePath = Application.streamingAssetsPath + configFile;
            string toFilePath = Application.persistentDataPath + configFile;
            string toFileDir = Application.persistentDataPath + configDir;
            try
            {
                WWW loadDB = new WWW(fromFilePath);
                while (!loadDB.isDone)
                {
                }
                if (!Directory.Exists(toFileDir))
                {
                    Directory.CreateDirectory(toFileDir);
                }
                File.WriteAllBytes(toFilePath, loadDB.bytes);
            }
            catch (System.Exception e)
            {
                Log.UILog(e.Message);
            }
        }

        private void LoadConfigFile(string file)
        {
#if UNITY_EDITOR
            DB db = new DB("Data Source=" + file);
#elif UNITY_ANDROID
            DB db = new DB("URI=file:" + file);
#else
            DB db = new DB(@"Data Source=" + file);
#endif

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
