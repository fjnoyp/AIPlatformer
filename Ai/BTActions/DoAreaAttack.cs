using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using Assets.Scripts.Interaction.Attacks;

namespace Assets.Scripts {
    class DoAreaAttack : Action {

        public OutwardAttack attack; 

        public override TaskStatus OnUpdate() {
            //immediately attack 
            attack.prepAttack();  //TODO ISSUE: ATTACKS MUST TAKE USER INPUT SINCE THERE COULD BE MULTIPLE USERS 
            return TaskStatus.Success; 
        }
    }
}
