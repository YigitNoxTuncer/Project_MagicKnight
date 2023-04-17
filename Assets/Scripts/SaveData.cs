using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;


namespace NOX
{
    [Serializable]
    public class SaveData
    {
        static SaveData instance;
        static bool isLoaded;

        public int currentSceneIndex;


        public static SaveData Instance()
        {
            if (!isLoaded)
            {
                Load();
                isLoaded = true;
            }
            return instance;
        }

        public static void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, Instance());
            stream.Close();
        }

        public static void Load()
        {
            string path = Application.persistentDataPath + "/player.save";
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                var data = formatter.Deserialize(stream) as SaveData;
                instance = data;
            }
            else
            {
                Debug.Log("Save file was not found in " + path);
                instance = new SaveData();
                instance.InitialLoad();
            }
        }

        public static void ResetSaveData()
        {
            instance.InitialLoad();
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.sav";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, Instance());
            stream.Close();
        }

        private void InitialLoad()
        {
            SetInitials();
        }


        private void SetInitials()
        {
            currentSceneIndex = 1;
        }
    }
}

