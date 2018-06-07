using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using Assets.Scripts.Ai.Pathfinding; 

namespace Assets.Scripts
{
    class MoveTowards : Action
    {
        //public SharedVector2 targetPos;
        public SharedTransform target;

        public DebugAi debugAi; 

        private CharacterMover controller;
        private LinkedListNode<Node> nextNode;

        //WARNING HARDCODED VARIABLE 
        private float minYOffset = 1.4f;

        private ITrajectoryPrediction trajectoryPrediction = new TrajectoryPrediction(); 

        private Vector2 _nextPos; 
        private Vector2 nextPos
        {
            get { return _nextPos; }
            set { _nextPos = value;
                debugAi.giveNextPos(_nextPos); 
            }
        }

        private IPathfinder pathfinder = new AStarFinder();


        public override void OnAwake()
        {
            //get character controller
            this.controller = GetComponent<CharacterMover>();
            this.debugAi = GetComponent<DebugAi>();
        }

        public override void OnStart()
        {
            //Debug.Log(target.Value); 
            updateNextNode(); 
        }

        private void updateNextNode() {
            //NodeManager is initialiazed with all nodes beforehand 

            LinkedList<Node> path = pathfinder.calculatePath(NodeManager.instance.getNearestNode(this.transform.position),
                NodeManager.instance.getNearestNode(target.Value.position));

            if (path != null) {
                this.nextNode = path.First;

                if (this.nextNode != null) {
                    this.nextPos = nextNode.Value.position;
                    debugAi.givePath(this.nextNode);
                }
            }
        }

        public override TaskStatus OnUpdate()
        {
            //while we are more than X distance from target node
            Vector2 ourPos = transform.position;
            float difX = Mathf.Abs(nextPos.x - ourPos.x);
            float difY = ourPos.y - nextPos.y;

            // || if platform above or below blocks us from nextPos
            if (this.nextNode == null ) {
                Debug.Log("task failed"); //10.16.16, no alternate behavior specified on task failure ... 
                this.nextNode = null; 
                return TaskStatus.Failure;
            }

            //Consider case disrupted movement causes ai to be directly below target
            if(difX < .1f && difY < 0)
            {
                updateNextNode();
                return TaskStatus.Running; 
            }

            //Still moving towards target 
            if (difX > .1f || difY > minYOffset)
            {
                float horizontalInput = 0;
                bool verticalInput = false;

                //Check if x movement necessary 
                if (Mathf.Abs(nextPos.x - ourPos.x) > .1f)
                {
                    horizontalInput = nextPos.x - ourPos.x;
                }
                //Check if jump necessary ====
                if (controller.IsGrounded())
                {
                    //if next <= ourPos.y and trajectory of no jump does not pass over 
                    if (nextPos.y < ourPos.y && !trajectoryPrediction.doesTrajectoryPassOver(Mathf.Sign(nextPos.x-ourPos.x)*controller.moveSpeed, 0, ourPos, nextPos)) {
                        verticalInput = true; 
                    }
                    //else if next is above our current position 
                    else if(nextPos.y > ourPos.y)
                    {
                        verticalInput = true; 
                    }
                }

                controller.setInput(horizontalInput, verticalInput);

                return TaskStatus.Running; 
            }
            //If reached nextNode, get next node to move towards
            else if ( (difX < .25f && Physics2D.Linecast(ourPos, nextPos + Vector2.up, 1 << LayerMask.NameToLayer("Environment"))) || nextNode.Next != null)
            {
                controller.setInput(0, false);

                if (controller.IsGrounded())
                {
                    updateNextNode();
                    if (this.nextNode == null) return TaskStatus.Failure; 
                    debugAi.givePath(this.nextNode);
                }


                return TaskStatus.Running;
            }
            //Done, no target path found 
            else
            {
                controller.setInput(0, false);
                return TaskStatus.Success;
            }
        }

        /* not working, don't know why ... 
        public override void OnDrawGizmos()
        {
            UnityEngine.Debug.Log("here"); 
            Gizmos.DrawLine(transform.position, nextPos); 
            Gizmos.DrawCube(nextPos, Vector2.one);

            LinkedListNode<Node> node = nextNode; 
            while(node.Next != null)
            {
                Gizmos.DrawCube(node.Value.position, node.Next.Value.position);
                node = node.Next; 
            }
        }
        */

    }
}
