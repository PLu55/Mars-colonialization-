using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Utilities;

namespace PLu.Mars.AI.HTN.Kernel
{
    /*
    public abstract class Sensor : MonoBehaviour, ISensor
    {
        //[SerializeField] private ScriptableObject _senses;
        [SerializeField] private float _tickInterval;
        private CountdownTimer _timer;
        private AIAgent _agent;
        private IContext _context;

        public IContext Context => _context;
        public float TickInterval => _tickInterval;

        protected void Start()
        {
            _agent = GetComponent<AIAgent>();
            Debug.Assert(_agent != null, "No AIAgent found on " + gameObject.name);
            _context = _agent.Context as Context;
            _timer = new CountdownTimer(_tickInterval);
            _timer.OnTimerStop += () => OnTimerStop();
            _timer.Start();
        }
        
        void Update()
        {
            _timer.Tick(Time.deltaTime);
        }

        public abstract void Tick(IContext _context);
        public abstract void DrawGizmos(IContext context);

        void OnTimerStop()
        {
            Tick(_context);
            _timer.Start();
        }
    }
    */
}