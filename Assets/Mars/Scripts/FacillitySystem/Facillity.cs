using System.Collections;
using System.Collections.Generic;
using PLu.Mars;
using UnityEngine;
 
namespace PLu.Mars.FacillitySystem
{
    public abstract class Facillity : MonoBehaviour, IFacillity
    {
        [SerializeField] private float _durabilityRate = 1f;
        [SerializeField] private float _durability = 1f;
        public float Durability { get; set; }

        private FacillityController _facillityController;
        void Start()
        {
            _facillityController = FacillityController.FindClosestFacillityController(transform.position);
            Debug.Assert(_facillityController != null, "Facillity Controller is not found in Facillity");
            _facillityController.AddFacillity(this);
        }
        void OnDestroy()
        {
            _facillityController.RemoveFacillity(this);
        }

        public virtual void UpdateFacillity(float updateInterval)
        {
            Durability -= _durabilityRate * updateInterval;
        }
    }
}
