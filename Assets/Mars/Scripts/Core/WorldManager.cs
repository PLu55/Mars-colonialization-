using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.Core
{   
    [DefaultExecutionOrder(-100)]    
    public class WorldManager : MonoBehaviour
    {
        [Header("Global Time Settings")]
        [SerializeField] double _globalTime = 0f;
        [Range(0,100)]  
        [SerializeField] float _timeScale = 1.0f;
        [SerializeField] double _timeOffset = 0.0;

        public static WorldManager Instance => _instance;
        public float deltaTime  => _deltaTime * (float)_timeScale;
        public double GlobalTime => _globalTime;
        public void SetTimeScale(float timeScale) => _timeScale = timeScale;
        public float DeltaTime => Time.deltaTime * (float)_timeScale;


        private static WorldManager _instance;
        private float _deltaTime;
        private GameObject _player;
        
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
            _globalTime = 0.0;
            _deltaTime = 0.0f;
        }
        void Update()
        {
            _deltaTime = Time.deltaTime * _timeScale;
            _globalTime += _deltaTime;
        }
    }
}
