using Assets.Scripts.Destructibles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderGizmosDrawer : MonoBehaviour
    {
        public Material material;

        public enum ColliderType
        {
            Polygon,
            Circle
        }
        public ColliderType colliderType;

        void OnDrawGizmos()
        {
            if (colliderType == ColliderType.Polygon)
            {
                MeshGenerator meshGenerator = new MeshGenerator();
                Polygon poly = new Polygon(GetComponent<PolygonCollider2D>().points);
                Mesh mesh = meshGenerator.CreateMesh(poly.getLocalToWorldPoints(this.transform).ToArray());
                material.SetPass(0);
                Graphics.DrawMeshNow(mesh, Vector3.zero, Quaternion.identity);
                DestroyImmediate(mesh);
            }
            else if (colliderType == ColliderType.Circle)
            {
                Gizmos.DrawSphere(this.transform.position, 1);
            }
        }
    }
}
