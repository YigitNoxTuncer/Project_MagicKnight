using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOX
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] GameObject magicMissile;

        private void OnMouseEnter()
        {
            magicMissile.transform.position = new Vector3(-2.85f, -0.1f, 0);
        }
    }
}


