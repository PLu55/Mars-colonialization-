using System.Collections;
using System.Collections.Generic;
using PLu.Mars.EnergySystem;
using PLu.Mars.InventorySystem;
using PLu.Utilities;
using UnityEngine;
 
namespace PLu.Mars.FacilitySystem
{
    public class Facility : PowerNode
    {
        //[SerializeField] protected Item[] _consumables;
        [SerializeField] protected Item _product;
        [SerializeField] protected float _productionPerTimeUnit = 1f;

        [SerializeField] protected float _updateInterval = 60f;
        [SerializeField] protected SerializableDictionary<Item, float> _consumables;


        //[SerializeField] private Dictionary<string, float> _consumables = new Dictionary<string, float>();
        

        public Dictionary<Item, float> Consumables => _consumables.ToDictionary();
        public Item Product => _product;
        public Inventory Inventory => _inventory;
        
        private Mesh _mesh;
        private RepeatTimer _updateTimer;
        private Inventory _inventory;
        private float _consumedEnergy = 0f;

        private new void Awake()
        {   
            _inventory = GetComponent<Inventory>();
            base.Awake();
        }
        void Start()
        {
            _consumedEnergy = 0f;
            _updateTimer = new RepeatTimer(_updateInterval);
            _updateTimer.OnTimerRepeat += UpdateFacility;
            _updateTimer.Start();
            _mesh = GetComponent<MeshFilter>().mesh;
        }
        void Update()
        {
            
        }
        private void UpdateFacility(float deltaTime)
        {

        }

        public override float UpdateEnergyBalance(float updateInterval, float effectBalance)
        {

            return NominalEffect * updateInterval;
        }
    }
}
