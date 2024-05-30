using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.Kernel
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        public static GameController Instance => _instance;
        public GameObject player;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            
        }
        void Update()
        {
            
        }
    }
}
