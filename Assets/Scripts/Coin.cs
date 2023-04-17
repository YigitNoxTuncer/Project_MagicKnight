using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] int coinValue = 1;
        PlayerController _playerController;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _playerController.totalCoins += coinValue;
        }
    }
}

