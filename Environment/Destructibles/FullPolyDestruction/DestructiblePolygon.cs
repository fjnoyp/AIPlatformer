//-*- java -*-
using UnityEngine;

using ClipperLib;

using System.Collections.Generic; //just for List<T>
using Assets.Scripts.Destructibles;
using Assets.Scripts.Environment.Destructibles;

/// <summary>
/// Arbitrarily destructible polygon.  Sub polygons created that do not have "anchor points" are physicalized and can fall down. 
/// </summary>
public class DestructiblePolygon : AbDestructible
{
    private Polygon polygon;
    public Material material;

    /// <summary>
    /// Points that prevent object from being physicalized 
    /// Should be in world coordinates ... 
    /// </summary>
    public List<GameObject> anchors;

    public override void destroy(Vector2 force, List<Vector2> points)
    {
        ExplosionCut(force, points);
    }

    /// <summary>
    /// Called by explosions to partially destroy this object.
    /// </summary>
    /// <param name="force"> Force to apply to this object </param>
    /// <param name="explosionPoints"> WORLD position of explosion points </param>
    /// <param name="clipType"> public enum ClipType { ctIntersection, ctUnion, ctDifference, ctXor }; </param>
    public void ExplosionCut(Vector2 force, List<Vector2> explosionPoints, ClipType clipType = ClipType.ctDifference)
    {
        Transform ourTransform = gameObject.transform;


        PolyClipper clipper = new PolyClipper();

        Polygon explosionPolygon = new Polygon(explosionPoints);
        List<Vector2[]> cutPolygons = clipper.ClipPoly(polygon.points.ToArray(), explosionPolygon.getWorldToLocalPoints(this.transform).ToArray(), clipType);

        if (cutPolygons.Count == 0)
        {
            Destroy(this.gameObject);
            return;
        }

        //Update children copies before determining if self is anchored 
        for (int i = 1; i < cutPolygons.Count; i++)
        {
            CreateCopy(cutPolygons[i]);
        }

        updateShape(cutPolygons[0]);
        determineAnchored(isAnchored());
    }

    //Create a new instance of this GameObject with "points" 
    private void CreateCopy(Vector2[] points)
    {
        GameObject newCopy = Instantiate(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
        newCopy.GetComponent<DestructiblePolygon>().initialize(isAnchored(), points);
    }

    private bool isAnchored()
    {
        return GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic;
    }

    public void initialize(bool parentAnchored, Vector2[] newPoints)
    {
        updateShape(newPoints);
        determineAnchored(parentAnchored);
    }

    public void determineAnchored(bool parentAnchored)
    {
        if (anchors.Count == 0) return; 

        Polygon worldPolygon = new Polygon(polygon.getLocalToWorldPoints(this.transform));

        if (parentAnchored)
        {
            bool isAnchored = false;
            for (int i = 0; i < anchors.Count; i++)
            {
                Polygon anchorPolygon = new Polygon(anchors[i].GetComponent<PolygonCollider2D>().points);
                if (worldPolygon.overlapsPoints(anchorPolygon.getLocalToWorldPoints(anchors[i].transform)))
                {
                    isAnchored = true;
                    break;
                }
            }
            if (!isAnchored)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                GetComponent<Rigidbody2D>().useAutoMass = true;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void updateShape(Vector2[] newPoints)
    {
        polygon = new Polygon(newPoints);

        //Create the new PolygonCollider2D
        GetComponent<PolygonCollider2D>().SetPath(0, newPoints);
        GetComponent<PolygonCollider2D>().density = 5;

        //Create the new mesh 
        MeshGenerator meshGenerator = new MeshGenerator();
        meshGenerator.SetupGOMesh(newPoints, material, this.gameObject);
    }

    void Start()
    {
        updateShape(GetComponent<PolygonCollider2D>().points);
    }

}
    //TODO: 
    // Weakenable structure that collapses without adequate support 
    // Logical ball-spring representation of structure, balls are deleted as the structure is destroyed.  Joints that break trigger cuts to the destructible polygon 
    //this structure would be a child transformed into destructible polygon 

//Issue : this simulates internal structure forces well, how to incorporate external forces? falling damage, trauma damage.  There would have to be an external force to ball structure force conversion OnCollisionEnter2D, transmits that force to the structure 
