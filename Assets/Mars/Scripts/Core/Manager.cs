using System.Collections;
using System.Collections.Generic;
using Codice.CM.SEIDInfo;
using UnityEngine;
using PLu.Mars.HabitatSystem;
using PLu.Utilities;
 
namespace PLu.Mars.Core
{
    public abstract class Manager : MonoBehaviour, IManager
    {
        [Header("Controller Settings")]
        [Tooltip("The controller update interval in seconds.")]
        [SerializeField] private float _updateInterval = 1f;
        public  HabitatManager HabitatManager => _habitatManager;
        public CountdownTimer UpdateTimer => _updateTimer;
        private HabitatManager _habitatManager;
        private CountdownTimer _updateTimer;


        void Awake()
        {
            _habitatManager = HabitatManager.FindClosestHabitat(transform.position);
            _updateTimer = new CountdownTimer(_updateInterval);
        }
        void Start()
        {
            _updateTimer.Start();
        }
        void Update()
        {
            _updateTimer.Tick(WorldManager.Instance.deltaTime);
            if (_updateTimer.IsFinished)
            {
                UpdateManager(_updateInterval);
                _updateTimer.Reset();
                _updateTimer.Start();
            }
        }
        public abstract void UpdateManager(float updateInterval);
    }
}
