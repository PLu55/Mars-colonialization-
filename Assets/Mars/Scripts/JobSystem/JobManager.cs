using System.Collections;
using System.Collections.Generic;
using PLu.Mars.HabitatSystem;
using UnityEngine;
 
namespace PLu.Mars.JobSystem
{
    public class JobManager : MonoBehaviour
    {
        private HabitatManager _habitatManager;
        void Awake()
        {
            _habitatManager = GetComponent<HabitatManager>();
        }
        void Start()
        {
            
        }
        void Update()
        {
            
        }
    }
}
