using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

/*
Sets SharedTranform target on start.  Returns failure if no healthpack was found or original healthpack was taken. 
*/

namespace Assets.Scripts.Ai {
    class SetTargetNearestHealth : Action{
        
        public SharedTransform target;

        private HealthPack[] healthPacks;
        private HealthPack minHealth; 

        public override void OnAwake() {
            healthPacks = GameObject.FindObjectsOfType<HealthPack>(); 
        }

        public override void OnStart() {
            float minDist = Mathf.Infinity;

            for(int i = 0; i<healthPacks.Length; i++) {
                float curDist = (healthPacks[i].transform.position - transform.position).sqrMagnitude; 
                if ( curDist < minDist) {
                    minDist = curDist;
                    minHealth = healthPacks[i]; 
                }
            }

            if(minHealth != null) {
                target.Value = minHealth.transform;

            }
            
        }

        public override TaskStatus OnUpdate() {
            if (minHealth == null || !minHealth.isActive) {
                return TaskStatus.Failure; 
            }
            return TaskStatus.Success; 
        }
    }
}

