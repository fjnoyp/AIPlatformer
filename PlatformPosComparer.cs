using System;
using System.Collections.Generic;
using UnityEngine; 

namespace Assets.Scripts
{
    public class PlatformPosComparer : IComparer<BoxCollider2D>
    {
        // > 0 if x is bigger
        // *1000 to avoid losing float info on int conversion
        public int Compare(BoxCollider2D x, BoxCollider2D y)
        {
            float xPos = x.transform.position.x;
            float yPos = y.transform.position.x;

            //if x is bigger platform return x > 0 
            if (Mathf.Approximately(xPos, yPos))
            {
                return (int)(1000 * x.bounds.size.x - 1000 * y.bounds.size.x);
            }
            //if x has bigger .x return x > 0
            else
            {
                return (int)(1000 * xPos - 1000 * yPos);
            }
        }
    }
}
