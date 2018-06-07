using UnityEngine; 
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

namespace Assets.Scripts.Ai.BTActions
{
    //WARNING: UNTESTED 
    //Given path, move 
    class FollowPath : Action
    {
        private CharacterMover controller;
        public LinkedListNode<Node> path; //TODO make this shared 

        //WARNING HARDCODED VARIABLE 
        private float minYOffset = 1.4f;

        private ITrajectoryPrediction trajectoryPrediction = new TrajectoryPrediction();

        private Vector2 nextPos { get; set; }

        public override TaskStatus OnUpdate()
        {
            {
                //while we are more than X distance from target node
                Vector2 ourPos = transform.position;
                float difX = Mathf.Abs(nextPos.x - ourPos.x);
                float difY = ourPos.y - nextPos.y;

                // || if platform above or below blocks us from nextPos
                if (this.path == null)
                {
                    Debug.Log("task failed"); //10.16.16, no alternate behavior specified on task failure ... 
                    this.path = null;
                    return TaskStatus.Failure;
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
                        if (nextPos.y < ourPos.y && !trajectoryPrediction.doesTrajectoryPassOver(Mathf.Sign(nextPos.x - ourPos.x) * controller.moveSpeed, 0, ourPos, nextPos))
                        {
                            verticalInput = true;
                        }
                        //else if next is above our current position 
                        else if (nextPos.y > ourPos.y)
                        {
                            verticalInput = true;
                        }
                    }

                    controller.setInput(horizontalInput, verticalInput);

                    return TaskStatus.Running;
                }
                //If reached nextNode, get next node to move towards
                else if ((difX < .25f && Physics2D.Linecast(ourPos, nextPos + Vector2.up, 1 << LayerMask.NameToLayer("Environment"))) || path.Next != null)
                {
                    controller.setInput(0, false);

                    if (controller.IsGrounded())
                    {
                        return TaskStatus.Success;
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

        }
    }
}
