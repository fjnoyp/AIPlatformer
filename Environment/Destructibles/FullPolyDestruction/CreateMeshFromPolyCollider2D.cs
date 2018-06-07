using UnityEngine;

/// <summary>
/// Genereate visible mesh for PolygonCollider2D 
/// </summary>
public class CreateMeshFromPolyCollider2D : MonoBehaviour
{

    private Mesh mesh;

    public PolygonCollider2D optPolygon;
    public Material optMaterial;

    void Start()
    {
        if (optPolygon != null)
        {
            MeshGenerator meshGenerator = new MeshGenerator();
            meshGenerator.SetupGOMesh(optPolygon.points, optMaterial, this.gameObject); 
        }
    }
}
