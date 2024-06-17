using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.BehaviourTrees
{
    public class Delay : Node
    {
        private readonly float duration;
        private float startTime;
        
        public Delay(float duration, int priority = 0) : base("Delay", priority){
            this.duration = duration;
        }
        
        public override Status Process() {
            if (startTime == 0) {
                startTime = Time.time;
            }
            
            if (Time.time - startTime >= duration) {
                startTime = 0;
                return Status.Success;
            }
            
            return Status.Running;
        }
        
        public override void Reset() {
            startTime = 0;
        }
    }
}
