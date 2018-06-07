using System;
using System.Collections.Generic;
using UnityEngine; 

namespace Assets.Scripts.Destructibles
{
    public class TempTestPolygon : MonoBehaviour
    {
        public PolygonCollider2D collider;
        public Transform transform;

        private List<GameObject> circles;
        private Polygon polygon; 

        public void Start()
        {
            circles = new List<GameObject>(); 
            foreach( Vector2 point in collider.points) {
                GameObject newGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                newGO.transform.position = point;

                circles.Add(newGO); 
            }
            polygon = new Polygon(collider.points); 
        }

        public void Update()
        {
            List<Vector2> points = polygon.getLocalToWorldPoints(transform); 

            for(int i = 0; i<points.Count; i++)
            {
                circles[i].transform.position = points[i]; 
            }
        }
    }
}
