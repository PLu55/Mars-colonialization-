using System.Collections;
using System.Collections.Generic;
using Codice.CM.SEIDInfo;
using UnityEngine;
using PLu.Mars.HabitatSystem;
using PLu.Utilities;
 
namespace PLu.Mars.Kernel
{
    public abstract class Controller : MonoBehaviour, IController
    {
        [Header("Controller Settings")]
        [Tooltip("The controller update interval in seconds.")]
        [SerializeField] private float _updateInterval = 1f;
        public  HabitatController HabitatController => _habitatController;
        public CountdownTimer UpdateTimer => _updateTimer;
        private HabitatController _habitatController;
        private CountdownTimer _updateTimer;


        void Awake()
        {
            _habitatController = HabitatController.FindClosestHabitat(transform.position);
            _updateTimer = new CountdownTimer(_updateInterval);
        }
        void Start()
        {
            _updateTimer.Start();
        }
        void Update()
        {
            _updateTimer.Tick(GameController.Instance.deltaTime);
            if (_updateTimer.IsFinished)
            {
                UpdateController(_updateInterval);
                _updateTimer.Reset();
                _updateTimer.Start();
            }
        }
        public abstract void UpdateController(float updateInterval);
    }
}
