using System;
using UnityEngine; 
using System.Collections.Generic;

namespace Assets.Scripts.Ai.Pathfinding 
{
    //WARNING: UNTESTED 

    //Datastructure storage AI path to target 
    public class Path : LinkedList<Node>
    {
        //Should be set by IPathFinder during path calculation 
        public float pathCost { get; set; }

        //Add new first node of path.  This ordering is allowed due to pathfinders adding nodes starting from last 
        public void addNodeToStart(Node node)
        {
            if(this.Count > 0)
            {
                float incrCost = 0; 
                node.tryGetNeighborCost(this.Last.Value, ref incrCost);
                pathCost += incrCost; 
            }
            AddFirst(node);             
        }

        //WARNING: UNTESTD 
        public Collider2D pathFirstHitCollider(int layerMask)
        {
            LinkedListNode<Node> node = this.First;

            while (node.Next != null) {
                Vector2 dir = node.Next.Value.position - node.Value.position;
                RaycastHit2D hit = Physics2D.Raycast(node.Value.position, dir, dir.magnitude, layerMask); 
                if (hit.collider != null)
                {
                    return hit.collider; 
                }
            }

            return null; 
        }

        //WARNING: UNTESTED 
        public List<Collider2D> pathHitColliders(int layerMask)
        {
            List<Collider2D> hitColliders = new List<Collider2D>(); 

            LinkedListNode<Node> node = this.First;

            while (node.Next != null) {
                Vector2 dir = node.Next.Value.position - node.Value.position;
                RaycastHit2D[] hits = Physics2D.RaycastAll(node.Value.position, dir, dir.magnitude, layerMask);

                foreach(RaycastHit2D hit in hits)
                {
                    if (hit.collider != null) hitColliders.Add(hit.collider); 
                }

            }

            return hitColliders; 
        }

        //Do nodes of path result in intersection with a certain collider? 
        public bool pathIntersects(int layerMask, BoxCollider2D collider2D)
        {
            return pathHitColliders(layerMask).Contains(collider2D); 
        }
    }
}
