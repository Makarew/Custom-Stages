using IdolShowdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace diorama
{
    internal class UParallax : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float parallaxEffect;

        private void Start ()
        {
            gameObject.AddComponent<Parallax>();
            gameObject.GetComponent<Parallax>().parallaxEffect = parallaxEffect;
        }
    }
}
