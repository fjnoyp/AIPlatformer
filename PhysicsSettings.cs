using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine; 

namespace Assets.Scripts
{
    class PhysicsSettings : MonoBehaviour 
    {
        public void Awake()
        {
            Physics2D.gravity = new Vector2(0, -9.8f); 
        }
    }
}
