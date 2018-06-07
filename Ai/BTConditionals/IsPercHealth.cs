using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

namespace Assets.Scripts.Ai {
    class IsPercHealth : Conditional {
 
        public float percHealthThreshold; 

        private Health health;
        private float thresholdHealth; 

        public override void OnAwake() {
            health = this.GetComponent<Health>();

            thresholdHealth = health.maxHealth * percHealthThreshold; 
        }
        public override TaskStatus OnUpdate() {

            if(health.curHealth < thresholdHealth) {
                return TaskStatus.Success;
            }
            else {
                return TaskStatus.Failure; 
            }
        }
    }
}
