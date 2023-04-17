using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class ChainsawTrapController : MonoBehaviour
    {
        [SerializeField] float rotationSpeed = 0.0f;

        void Update()
        {
            LaserRotation();
        }

        private void LaserRotation()
        {
            transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

}

