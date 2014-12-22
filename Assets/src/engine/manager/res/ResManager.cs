﻿
using engine.core;
using Mono.Data.Sqlite;
using UnityEngine;

namespace engine.manager
{
    public class ResManager
    {

        public ResManager()
        {
            loadConfigFile();
        }

        private void loadConfigFile()
        {
            DB db = new DB(@"Data Source=" + Application.dataPath + "/Resources/config/config.sqlite");
            SqliteDataReader reader = db.ReadFullTable("config");
            while (reader.Read())
            {
                Log.log(reader.GetString(0));
                Log.log(reader.GetString(1));
            }
            //db.ChangePassword("");
            reader.Close();
            db.CloseSqlConnection();
        }

        public Object load(string name, LoadType t = LoadType.RESOURCE)
        {
            return null;
        }

    }
}