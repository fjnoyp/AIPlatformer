using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Destructibles
{
    /// <summary>
    /// Class to manage storage and transformation of points in space 
    /// </summary>
    public class Polygon
    {
        public List<Vector2> points {
            private set;
            get;
        }

        public Polygon(Vector2[] points)
        {
            this.points = new List<Vector2>(points); 
        }
        public Polygon(List<Vector2> points)
        {
            this.points = points; 
        }

        public List<Vector2> getWorldToLocalPoints(Transform transform)
        {
            List<Vector2> transformedPoints = new List<Vector2>(points.Count); 

            for(int i = 0; i<points.Count; i++)
            {
                transformedPoints.Add(transform.InverseTransformPoint(points[i])); 
            }

            return transformedPoints; 
        }

        public List<Vector2> getLocalToWorldPoints(Transform transform)
        {
            List<Vector2> transformedPoints = new List<Vector2>(points.Count); 

            for(int i = 0; i<points.Count; i++)
            {
                transformedPoints.Add(transform.TransformPoint(points[i])); 
            }

            return transformedPoints; 
        }

        public bool overlapsPoly(Polygon otherPolygon)
        {
            return overlapsPoints(otherPolygon.points); 
        }
        public bool overlapsPoints(List<Vector2> points)
        {
            return overlapsPoints(points.ToArray()); 
        }
        public bool overlapsPoints(Vector2[] points)
        {
            PolyClipper clipper = new PolyClipper();
            return (clipper.ClipPoly(this.points.ToArray(), points, ClipperLib.ClipType.ctIntersection).Count > 0); 
        }

    }
}
