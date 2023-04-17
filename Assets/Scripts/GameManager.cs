using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

namespace NOX
{
    public class GameManager : MonoBehaviour
    {
        PlayerController _playerController;
        Generator _generator;
        GameOverPanelIdentfier _gameOverPanel;
        VictoryPanelIdentfier _victoryPanel;
        ClickToStartPanelIdentfier _clickToStartPanelIdentfier;
        AbilityButtonIdentfier _abilityButton;
        SaveLoadManager _saveLoadManager;


        [SerializeField] string nextScene;
        [SerializeField] int coinsNeededForNextLevel;

        bool _isVictory;

        AudioSource _audioSource;
        [SerializeField] AudioClip victoryTrack;
        [SerializeField] AudioClip gameOverTrack;



        private void Start()
        {
            IdentifyCaches();

            Time.timeScale = 0;

            _gameOverPanel.gameObject.SetActive(false);
            _victoryPanel.gameObject.SetActive(false);
        }

        private void Update()
        {
            HandlePlayerDeath();
            HandleVictory();
            HandleAbilityUsed();
            TapToStart();
        }


        private void HandlePlayerDeath()
        {
            if (_playerController.isDead)
            {
                _gameOverPanel.gameObject.SetActive(true);
                _audioSource.Stop();
                AudioSource.PlayClipAtPoint(gameOverTrack, Vector3.zero, 0.015f);
            }
        }

        private void HandleVictory()
        {
            if (_playerController.totalCoins >= coinsNeededForNextLevel)
            {
                _isVictory = true;
                _victoryPanel.gameObject.SetActive(true);
                _audioSource.Stop();
                AudioSource.PlayClipAtPoint(victoryTrack, Vector3.zero, 0.05f);
                Time.timeScale = 0;
            }
        }

        private void HandleAbilityUsed()
        {
            if (_generator.isAbilityUsed)
            {
                _abilityButton.gameObject.SetActive(false);
            }
        }

        private void TapToStart()
        {
            if (Input.GetButton("Fire1") && !_isVictory)
            {
                _clickToStartPanelIdentfier.gameObject.SetActive(false);
                Time.timeScale = 1;
                _saveLoadManager.SaveGame();
            }
        }

        public void RetryButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextLevelButton()
        {
            SceneManager.LoadScene(nextScene);
        }

        public void QuitButton()
        {
            SceneManager.LoadScene(0);
        }

        private void IdentifyCaches()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _generator = FindObjectOfType<Generator>();
            _gameOverPanel = FindObjectOfType<GameOverPanelIdentfier>();
            _victoryPanel = FindObjectOfType<VictoryPanelIdentfier>();
            _clickToStartPanelIdentfier = FindObjectOfType<ClickToStartPanelIdentfier>();
            _abilityButton = FindObjectOfType<AbilityButtonIdentfier>();
            _saveLoadManager = GetComponent<SaveLoadManager>();
            _audioSource = GetComponent<AudioSource>();
        }
    }
}

