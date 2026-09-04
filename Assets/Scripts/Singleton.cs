using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        private void Awake()
        {
            // Create the singleton instance if none exists.
            if (Instance == null)
            {
                Instance = GetComponent<T>();
            }
            else
            {
                // Destroy duplicate instances.
                Destroy(gameObject);
            }
        }
    }
}