using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System;
using System.Collections.Generic;
using Assets.Scripts.Interaction.Attacks;

namespace Assets.Scripts.Ai.BTConditionals
{
    //WARNING: UNTESTED 
    public class EnemyWindingUpAttack : Conditional 
    {
        public SharedTransform target;

        public override TaskStatus OnUpdate()
        {
            if (target.Value.gameObject.GetComponent<MeleeAttack>().isWindingUp) return TaskStatus.Success;
            else return TaskStatus.Failure; 
        }
    }
}
