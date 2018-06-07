using System;
using UnityEngine; 


namespace Assets.Scripts
{
    class Testing : MonoBehaviour
    {
        public Collider2D colliderA;
        public Collider2D colliderB;

        public void Update()
        {
            Debug.Log(colliderA.IsTouching(colliderB) + " " + colliderB.IsTouching(colliderA));
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log("triggered stay"); 
        }
    }
}
