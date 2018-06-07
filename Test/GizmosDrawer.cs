using System;
using System.Collections.Generic;
using UnityEngine; 

namespace Assets.Scripts
{
    public class GizmosDrawer : MonoBehaviour
    {
        public bool showDebugPoints;
        public bool showDebugPaths;
        public bool showNodes;
        public bool showNodeNeighbors; 

        //Storage of debug points to display
        private List<Vector2> debugPoints = new List<Vector2>();

        //Storage of hyperbola jumps 
        private List<List<Vector2>> debugPaths = new List<List<Vector2>>();

        //Draw nodes as spheres with neighbor connections 
        private Dictionary<int, List<Node>> nodes;

        public void clear()
        {
            if (debugPoints!=null)this.debugPoints.Clear();
            if(debugPaths!=null)this.debugPaths.Clear();
            if(this.nodes!=null) this.nodes.Clear(); 
        }

        public void addDebugPoint(Vector2 debugPoint)
        {
            debugPoints.Add(debugPoint); 
        }
        public void addPath(List<Vector2> path)
        {
            debugPaths.Add(path); 
        }
        public void setNodes(Dictionary<int, List<Node>> nodes)
        {
            this.nodes = nodes; 
        }

        public void addHyperbola(float moveSpeed, float jumpForce, float xRange, Vector2 startPos)
        {
            List<Vector2> debugPath = new List<Vector2>();

            //current time and position
            float t;
            float x1 = startPos.x;
            float y1 = startPos.y;
            float x2 = x1; // + 1;
            float y2;

            //track max render 
            float count = 0;
            float max = Mathf.Abs(xRange);
            float increment = 1.0f;

            float moveDir = increment * (Mathf.Abs(moveSpeed) / moveSpeed);
            while (count <= max)
            {
                //evaluate for t 
                t = (x2 - x1) / moveSpeed;
                y2 = y1 + jumpForce * t + .5f * Physics2D.gravity.y * Mathf.Pow(t, 2);
                debugPath.Add(new Vector2(x2, y2));

                x2 += moveDir;
                count += increment;
            }
            addPath(debugPath); 
        }

        public void OnDrawGizmos()
        {
            if (showDebugPoints)
            {
                Gizmos.color = new Color(1, 0, 0);
                for (int i = 0; i < debugPoints.Count; i++)
                {
                    Gizmos.DrawCube(debugPoints[i], new Vector2(1f, 1f));
                }
                Gizmos.color = new Color(1, 1, 1);
            }

            //Debug.Log("show: " + showJumpTrajectories); 
            if (showDebugPaths)
            {
                {
                    //debug paths 
                    foreach (List<Vector2> path in debugPaths)
                    {
                        if (path.Count != 0)
                        {
                            Vector2 previousPoint = path[0];
                            for (int i = 1; i < path.Count; i++)
                            {
                                Gizmos.DrawLine(previousPoint, path[i]);
                                previousPoint = path[i];
                            }
                        }
                    }
                }
            }

            //display nodes
            if (showNodes || showNodeNeighbors)
            {
                foreach (var pair in nodes)
                {
                    List<Node> nodeList = pair.Value;
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        Node curNode = nodeList[i];
                        if (showNodes) Gizmos.DrawSphere(curNode.position, .1f);

                        if (showNodeNeighbors)
                        {
                            foreach(Node neighbor in curNode.neighbors.Keys)
                            {
                                GizmosDrawArrow(curNode.position, neighbor.position, .2f);
                            }
                        }
                    }
                }
            }
        }

        private void GizmosDrawArrow(Vector2 start, Vector2 to, float size)
        {
            Gizmos.DrawLine(start, to);
            float angle = Mathf.Atan2(to.y - start.y, to.x - start.x);
            float arrowAngle = (180 - 45) * Mathf.Deg2Rad;
            Gizmos.DrawLine(to, to + size * new Vector2(Mathf.Cos(angle + arrowAngle), Mathf.Sin(angle + arrowAngle)));
            arrowAngle += 90 * Mathf.Deg2Rad;
            Gizmos.DrawLine(to, to + size * new Vector2(Mathf.Cos(angle + arrowAngle), Mathf.Sin(angle + arrowAngle)));
        }

        

    }
}
