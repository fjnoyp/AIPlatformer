using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

/*
Sets SharedTranform target on start.  Returns failure if no healthpack was found or original healthpack was taken. 
*/

namespace Assets.Scripts.Ai {
    class SetTargetEnemy : Action {

        public SharedTransform target;

        private GameObject[] enemies;
        private GameObject nearestEnemy;

        public override void OnAwake() {
            string tag = (this.gameObject.tag == "Enemy") ? "Player" : "Enemy";
            enemies = GameObject.FindGameObjectsWithTag(tag); 
        }

        public override void OnStart() {
            nearestEnemy = AiUtilities.getNearest(transform.position, enemies);
            target.Value = nearestEnemy.transform;
        }

        public override TaskStatus OnUpdate() {

            if (nearestEnemy == null)
                return TaskStatus.Failure;

            return TaskStatus.Success;
        }
    }
}


