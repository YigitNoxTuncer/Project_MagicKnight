using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace NOX
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] Rigidbody2D magicMissileMainMenuRigidbody;
        [SerializeField] int magicMissileForce;

        [SerializeField] Animator playerMainMenuAnimator;

        int _currentSceneIndex;

        private void Start()
        {
            Time.timeScale = 1;
            _currentSceneIndex = SaveData.Instance().currentSceneIndex;
        }

        public void ShootMagicMissile()
        {
            magicMissileMainMenuRigidbody.AddForce(new Vector2(magicMissileForce, 0));
        }

        public void PlayGame()
        {
            StartCoroutine(LoadSceneDelay());
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private IEnumerator LoadSceneDelay()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(_currentSceneIndex);
        }
    }
}

