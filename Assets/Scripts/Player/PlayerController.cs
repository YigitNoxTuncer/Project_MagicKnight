using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace NOX
{
    public class PlayerController : MonoBehaviour
    {

        [Header("Caches")]
        Generator _GeneratorScript;
        Rigidbody2D _playerRigidbody;
        Animator _playerAnimatorManager;

        [Header("Ground Detection")]
        [SerializeField] Transform groundCheckTransform;
        bool _isGrounded;
        [SerializeField] LayerMask groundCheckLayerMask;

        [Header("Magic Jet Settings")]
        [SerializeField] float magicJetForce = 75.0f;

        [Header("Magic Jet Animators")]
        [SerializeField] Animator jetBeginAnimator;
        [SerializeField] Animator jetStreamAnimator;

        [Header("Player Stats")]
        [SerializeField] float deadSpeed;
        public bool isDead = false;
        public int totalCoins = 0;
        [Space]
        public bool isLevelOne;
        public bool isLevelTwo;
        public bool isLevelThree;
        public int  currentLevelIndex;


        [Header("Magic Shield Settings")]
        [SerializeField] GameObject magicShield;
        [SerializeField] float magicShieldTimer = 3.0f;
        [SerializeField]bool _isMagicShieldActive = false;

        [Header("UI Elements")]
        CoinsTextIdentfier _coinsText;

        private void Start()
        {
            IdentifyCaches();
        }

        private void FixedUpdate()
        {
            MagicJetControl();
            UpdateGroundedStatus();
        }

        private void Update()
        {
            MagicJetAnimations();
            UpdateUI();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle") && !_isMagicShieldActive)
            {
                HitByObstacle(collision);
            }

            if (collision.CompareTag("Coin"))
            {
                CollectCoin(collision);
            }

            if (collision.CompareTag("MagicShieldPickUp"))
            {
                CollectMagicShield(collision);
            }

        }

        private void UpdateGroundedStatus()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);

            _playerAnimatorManager.SetBool("isGrounded", _isGrounded);
        }
        #region Magic Jet Settings
        private void MagicJetControl()
        {
            bool magicJetActive = Input.GetButton("Fire1");
            magicJetActive = magicJetActive && !isDead;

            if (magicJetActive)
            {
                _playerRigidbody.AddForce(new Vector2(0, magicJetForce));
            }

            if (isDead)
            {
                _playerRigidbody.AddForce(new Vector2(deadSpeed, 0));
            }
        }

        private void MagicJetAnimations()
        {

            if (!isDead)
            {
                if (Input.GetButton("Fire1"))
                {
                    _playerAnimatorManager.Play("Jump");
                    jetBeginAnimator.SetBool("isFired", true);
                    jetStreamAnimator.SetBool("isFired", true);
                }

                if (Input.GetButtonUp("Fire1"))
                {
                    jetBeginAnimator.SetBool("isFired", false);
                    jetStreamAnimator.SetBool("isFired", false);
                }
            }

            else
            {
                jetBeginAnimator.SetBool("isFired", false);
                jetStreamAnimator.SetBool("isFired", false);

                _playerAnimatorManager.SetBool("isDead", true);
            }

        }
        #endregion

        private void MagicShieldPower()
        {
            if (_isMagicShieldActive)
            {
                StartCoroutine(MagicShieldProtectionTimer());
            }
            else
            {
                StopCoroutine(MagicShieldProtectionTimer());
            }

        }

        private void HitByObstacle(Collider2D obstacleCollider)
        {
            isDead = true;
        }

        private void CollectCoin(Collider2D coinCollider)
        {
            Destroy(coinCollider.gameObject);
        }

        private void CollectMagicShield(Collider2D magicShieldCollider)
        {
            magicShield.SetActive(true);
            _isMagicShieldActive = true;
            _GeneratorScript.CheckIfMagicShieldCanSpawn();
            MagicShieldPower();
            Destroy(magicShieldCollider.gameObject);
        }

        private void UpdateUI()
        {
            _coinsText.GetComponent<TextMeshProUGUI>().text = "Coins:" + totalCoins;
        }

        private void IdentifyCaches()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();
            _playerAnimatorManager = GetComponent<Animator>();
            _GeneratorScript = GetComponent<Generator>();
            _coinsText = FindObjectOfType<CoinsTextIdentfier>();
        }


        private IEnumerator MagicShieldProtectionTimer()
        {
            yield return new WaitForSeconds(magicShieldTimer);
            magicShield.SetActive(false);   
            _isMagicShieldActive = false;
        }
    }
}

