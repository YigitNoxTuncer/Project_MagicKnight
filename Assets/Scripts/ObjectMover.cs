using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace NOX
{
    public class ObjectMover : MonoBehaviour
    {
        [SerializeField]PlayerController _playerController;

        [SerializeField] float level1Speed;
        [SerializeField] float level2Speed;
        [SerializeField] float level3Speed;

        public float speed;

        private float _x;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            AdjustSpeedToLevel();
        }

        void Update()
        {
            MoveObject();
        }

        private void MoveObject()
        {
            _x = transform.position.x;
            _x += speed * Time.deltaTime;
            transform.position = new Vector3(_x, transform.position.y, transform.position.z);
        }

        private void AdjustSpeedToLevel()
        {
            if (_playerController.isLevelOne)
            {
                speed = level1Speed;
            }
            else if( _playerController.isLevelTwo)
            {
                speed = level2Speed;
            }
            else if (_playerController.isLevelThree)
            {
                speed = level3Speed;
            }
        }
    }
}