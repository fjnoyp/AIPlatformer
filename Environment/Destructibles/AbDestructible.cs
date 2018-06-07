using System;
using System.Collections.Generic;
using UnityEngine; 

namespace Assets.Scripts.Environment.Destructibles
{
    public abstract class AbDestructible : MonoBehaviour
    {
        public abstract void destroy(Vector2 force, List<Vector2> points); 
    }

}
