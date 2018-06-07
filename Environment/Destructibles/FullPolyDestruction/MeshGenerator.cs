using UnityEngine; 
/// <summary>
//Generate visual mesh from points 
/// </summary>
public class MeshGenerator 
{
    public Mesh CreateMesh(Vector2[] newPoints)
    {
        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(newPoints);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[newPoints.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(newPoints[i].x, newPoints[i].y, 0);
        }

        // Create the New Mesh 
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        //Set up texture coordinate for New Mesh 
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        }
        mesh.uv = uvs;

        return mesh;
    }

    public void SetupGOMesh(Vector2[] newPoints, Material material, GameObject GO)
    {
        Mesh mesh = CreateMesh(newPoints); 

        // Set up game object with mesh;
        if (GO.GetComponent<MeshRenderer>() == null)
            GO.AddComponent(typeof(MeshRenderer));

        if (GO.GetComponent<MeshFilter>() == null)
            GO.AddComponent(typeof(MeshFilter));

        MeshFilter filter = GO.GetComponent<MeshFilter>() as MeshFilter;
        filter.mesh = mesh;
        MeshRenderer mRenderer = GO.GetComponent<MeshRenderer>() as MeshRenderer;
        mRenderer.material = material;

        //NOTE HARD CODING !!! 
        mRenderer.sortingLayerName = "Foreground";
    }
}
