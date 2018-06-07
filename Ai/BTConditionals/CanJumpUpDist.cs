using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

namespace Assets.Scripts.Ai.BTConditionals {

    //WARNING: UNTESTED 
    class CanJumpUpDist : Conditional {

        public float dist; 

        public override void OnAwake() {
        }
        public override TaskStatus OnUpdate() {

            //do raycast upwards on environment layer 
            //check returned distance, return if greater than dist 
                return TaskStatus.Success;
                return TaskStatus.Failure; 
        }
    }
}

