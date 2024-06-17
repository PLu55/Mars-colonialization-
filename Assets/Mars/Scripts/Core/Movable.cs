using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common;
using UnityEngine;
using UnityEngine.AI;

namespace PLu.Mars.Core
{
    public class Movable : MonoBehaviour, IMovable
    {
        public float Speed { get; set; }
        public float Acceleration { get; set; }
        public float RotationSpeed { get; set; }
        public float RotationAcceleration { get; set; }
        public float MaxSpeed { get; set; }
        public float MaxRotationSpeed { get; set; }
        public float MaxAcceleration { get; set; }
        public float MaxRotationAcceleration { get; set; }

        private Rigidbody _rigidbody;
        private NavMeshAgent _navMeshAgent;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Debug.Assert(_rigidbody != null, $"Rigidbody not found on object {gameObject.name}");
            _rigidbody.isKinematic = true;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            Debug.Assert(_navMeshAgent != null, $"NavMeshAgent not found on object {gameObject.name}");

        }
        void Start()
        {
            GameObject target = GameObject.Find("Target");
            Debug.Assert(target != null, "Target not found");
            _navMeshAgent.SetDestination(target.transform.position);
        }
        public void SetDestination(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
        }

        public bool IsAtDestination()
        {
            return !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
        }
    }
}
