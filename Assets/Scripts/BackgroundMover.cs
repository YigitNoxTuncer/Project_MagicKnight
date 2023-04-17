using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class BackgroundMover : MonoBehaviour
    {
        public float speed;
        private float _x;
        public float destinationPoint;
        public float originalPoint;

        void Update()
        {
            MoveBackground();
        }

        private void MoveBackground()
        {
            _x = transform.position.x;
            _x += speed * Time.deltaTime;
            transform.position = new Vector3(_x, transform.position.y, transform.position.z);



            if (_x <= destinationPoint)
            {
                _x = originalPoint;
                transform.position = new Vector3(_x, transform.position.y, transform.position.z);
            }
        }
    }
}


