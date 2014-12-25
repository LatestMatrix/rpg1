
using engine.core;
using Mono.Data.Sqlite;
using UnityEngine;

namespace engine.manager
{
    public class ResManager
    {

        public ResManager()
        {
            LoadConfigFile();
            LoadUI();
        }

        private void LoadConfigFile()
        {
            DB db = new DB(@"Data Source=" + Application.dataPath + "/Resources/config/config.sqlite");
            SqliteDataReader reader = db.ReadFullTable("config");
            while (reader.Read())
            {
                Log.Trace(reader.GetString(0));
                Log.Trace(reader.GetString(1));
            }
            //db.ChangePassword("");
            reader.Close();
            db.CloseSqlConnection();
        }

        private void LoadUI()
        {
            GameObject obj = Resources.Load<GameObject>("ui/joystick");
            NGUITools.AddChild(NGUITools.FindCameraForLayer((obj).layer).gameObject, obj);
            obj = Resources.Load<GameObject>("ui/skillArea");
            NGUITools.AddChild(NGUITools.FindCameraForLayer((obj).layer).gameObject, obj);
        }

        public GameObject Load(string name)
        {
            return Resources.Load<GameObject>(name);
        }

    }
}
