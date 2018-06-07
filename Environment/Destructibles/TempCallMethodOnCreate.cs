using Assets.Scripts.Environment.Destructibles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Destructibles
{
    public class TempCallMethodOnCreate : MonoBehaviour
    {
        public DestructibleRock dr;
        public void Start()
        {
            dr.destroy(Vector2.zero, null); 
        }
    }
}
