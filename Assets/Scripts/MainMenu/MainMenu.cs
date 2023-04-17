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

        PlayButton _playButton;
        ResetSaveButton _resetSaveButton;
        QuitButton _quitButton;

        private void Start()
        {
            IdentifyCaches();

            Time.timeScale = 1;
            _currentSceneIndex = SaveData.Instance().currentSceneIndex;
        }

        public void ShootMagicMissile()
        {
            magicMissileMainMenuRigidbody.AddForce(new Vector2(magicMissileForce, 0));
            DisableButtons();
        }

        public void PlayGame()
        {
            StartCoroutine(LoadSceneDelay());
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void DisableButtons()
        {
            _playButton.gameObject.SetActive(false);
            _resetSaveButton.gameObject.SetActive(false);
            _quitButton.gameObject.SetActive(false);
        }

        private void IdentifyCaches()
        {
            _playButton = FindObjectOfType<PlayButton>();
            _resetSaveButton = FindObjectOfType<ResetSaveButton>();
            _quitButton = FindObjectOfType<QuitButton>();
        }

        private IEnumerator LoadSceneDelay()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(_currentSceneIndex);
        }
    }
}

