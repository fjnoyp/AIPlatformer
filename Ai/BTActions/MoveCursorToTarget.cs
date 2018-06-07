using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using Assets.Scripts.Interaction.Attacks;

namespace Assets.Scripts.Ai.BTActions {
    class MoveCursorToTarget : Action {
        public SharedTransform target;
        public Transform cursor; 

        public override TaskStatus OnUpdate() {
            cursor.position = target.Value.position; 
            return TaskStatus.Success; 
        }

    }
}
