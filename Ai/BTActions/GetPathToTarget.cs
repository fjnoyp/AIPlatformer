using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using Assets.Scripts.Ai.Pathfinding; 

namespace Assets.Scripts.Ai.BTActions
{
    /*
Should moveToward contain all move logic or should there be separate nodes for all aspects

like isGrounded, isXDist, isYDist, doesPassOver

Node based gives reuse flexibility - will moveToward logic vary significantly or need to be reused flexibly? - currently not needed to be flexible, keep current system (except move path calculation out as we should be doing separate calculations on path) 
*/

    //WARNING: UNTESTED 
    class GetPathToTarget : Action 
    {
        private ITrajectoryPrediction trajectoryPrediction = new TrajectoryPrediction();
        private IPathfinder pathfinder = new AStarFinder(); 

        public SharedTransform target;
        public LinkedList<Node> path; //TODO: make this shared 

        public override void OnAwake()
        {
            base.OnAwake();
        }
        public override void OnStart()
        {
            base.OnStart();
        }
        public override TaskStatus OnUpdate()
        {
            return base.OnUpdate();
        }
        private void getPath()
        {
            path = pathfinder.calculatePath(NodeManager.instance.getNearestNode(this.transform.position), NodeManager.instance.getNearestNode(target.Value.position));

        }
    }
}
