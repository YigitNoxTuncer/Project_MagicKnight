using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace NOX
{
    public class SaveLoadManager : MonoBehaviour
    {
        [SerializeField] PlayerController player;

        void Start()
        {
            player = FindObjectOfType<PlayerController>();

        }

        public void ResetSave()
        {
            SaveData.ResetSaveData();
            SceneManager.LoadScene(0);
            SaveData.Save();
        }

        public void SaveGame()
        {
            SaveData.Instance().currentSceneIndex = player.currentLevelIndex;
            SaveData.Save();
        }

        public void LoadGame()
        {
            SaveData.Load();
        }
    }
}
