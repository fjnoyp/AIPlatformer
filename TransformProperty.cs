using System;
using UnityEngine; 

namespace Assets.Scripts
{
    //exposes properties of Transform for Behavior Tree 
    class TransformProperty : MonoBehaviour 
    {
        public Vector2 position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }
    }
}
