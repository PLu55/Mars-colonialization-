using System.Collections;
using System.Collections.Generic;
using PLu.Mars;
using UnityEngine;
 
namespace PLu
{
    public class HabitatTest1 : MonoBehaviour
    {
        [SerializeField] private double _localTime;

        private HabitatController _habitatController;

        void Awake()
        {
            _habitatController = FindObjectOfType<HabitatController>();
            Debug.Assert(_habitatController != null, "Habitat Controller is not found in EnergyController");
        }
        void Start()
        {
            
        }
        void Update()
        {
            _localTime = _habitatController.LocalTime;
        }
    }
}
