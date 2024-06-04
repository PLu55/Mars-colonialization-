using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.Kernel
{
    public interface IMovable
    {
	    float Speed { get; set; }
        float Acceleration { get; set; }
        float RotationSpeed { get; set; }
        float RotationAcceleration { get; set; }
        float MaxSpeed { get; set; }
        float MaxRotationSpeed { get; set; }
        float MaxAcceleration { get; set; }
        float MaxRotationAcceleration { get; set; }

        void SetDestination(Vector3 destination);
        bool IsAtDestination();
    }
}
