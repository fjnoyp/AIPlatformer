using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using Assets.Scripts.Interaction.Attacks;

namespace Assets.Scripts {
    class DoMeleeAttack : Action {

        public MeleeAttack attack;
        public SharedTransform target; 

        public override TaskStatus OnUpdate() {
            //immediately attack
            attack.prepAttack();
            attack.releaseAttack(); 
            return TaskStatus.Success; 
        }
    }
}
