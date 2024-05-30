using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.Kernel;
 
namespace PLu.Mars.EnergySystem
{
    public class PowerNode : MonoBehaviour, IPowerNode
    {
        [SerializeField] protected PowerNodeType _powerNodeType = PowerNodeType.PowerConsumer;
        [SerializeField] private GameObject _habitat;
        [SerializeField] private float _nominalEffect = 0f;


        private EnergyController _energyController;
        public GameObject Habitat => _habitat;
        public PowerNodeType PowerNodeType => _powerNodeType;
        public float NominalEffect => _nominalEffect;
    
        public EnergyController EnergyController => _energyController;

        void Start()
        {
            Debug.Log("Power Node is started");
            Debug.Assert(_habitat != null, "Habitat is not assigned to PowerConsumer");

            _energyController = _habitat.GetComponent<EnergyController>();
            Debug.Assert(_energyController != null, "Energy Controller is not found in PowerNode");
            _energyController.AddPowerNode(this);
        }
        public virtual float CurrentEffectLevel()
        {
            return _nominalEffect;
        }
    }
}
