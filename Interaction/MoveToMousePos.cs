using System;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public class MoveToMousePos : MonoBehaviour
    {

        public void Update()
        {
            Vector3 target; 
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            this.transform.position = target;  
        }
    }
}
