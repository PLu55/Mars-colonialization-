using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluidHTN;
using PLu.Utilities;
using PLu.Mars.Core;
using PLu.Mars.AI.HTN;
using PLu.Mars.AI.HTN.Domains.Transporter;
using PLu.Mars.FacilitySystem;

namespace PLu.Mars.TransportSystem
{
    public class TransporterController : MonoBehaviour
    {
        [Header("Updating")]
        [SerializeField] private float _updateInterval = 60f;
        [Header("Debugging")]
        [SerializeField] private bool _debug = false;

        private List<TransporterAgent> _transporters = new List<TransporterAgent>();
        private CountdownTimer _TickTimer;
        void Start()
        {
            _TickTimer = new CountdownTimer(_updateInterval);
            _TickTimer.Start();
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Transporter");
            foreach (GameObject go in gos)
            {
                TransporterAgent agent = go.GetComponent<TransporterAgent>();
                if (agent != null)
                {
                    _transporters.Add(agent);
                }
            }
            Test1();
        }
        void Update()
        {
            _TickTimer.Tick(WorldManager.Instance.deltaTime);
            if (_TickTimer.IsFinished)
            {
                UpdateTransportController();
                _TickTimer.Reset();
                _TickTimer.Start();
            }
        }
        private void UpdateTransportController()
        {
            if (_debug) Debug.Log("TransporterController.UpdateTransportController()");
        }
        public bool FindClosestTransporter(Vector3 position, out TransporterAgent closest)
        {
            closest = null;
            float minDistance = float.MaxValue;
            foreach (TransporterAgent agent in _transporters)
            {
                float distance = Vector3.Distance(agent.transform.position, position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = agent;
                }
            }
            return closest != null;
        }

        public void Test1()
        {
            Debug.Log("TransporterController.Test1()");
            TransporterAgent closest;
            if (FindClosestTransporter(Vector3.zero, out closest))
            {
                Debug.Log($"Closest transporter: {closest}");
                Store[] stores =  FindObjectsOfType<Store>();
                if (stores.Length > 0)
                {
                    Vector3 pickupLocation = stores[0].transform.position;
                    Vector3 dropoffLocation = stores[1].transform.position;
                    closest.Context.SetTransporterJob(pickupLocation,  dropoffLocation);
                    //closest.Context.SetState(AIWorldState.HasJob, true, EffectType.PlanAndExecute);
                    byte value = closest.Context.GetState(AIWorldState.HasJob);
                    Debug.Log($"Transporter has job: {value}");
                }
                else
                {
                    Debug.Log("No store found");
                }
            }
            else
            {
                Debug.Log("No transporter found");
            }
        }
    }
}
