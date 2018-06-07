//-*-java-*-
using UnityEngine; 

using ClipperLib; 
using System.Collections.Generic; //just for List<T>

//wtf unity... 
using Path = System.Collections.Generic.List<ClipperLib.IntPoint>;
using Paths = System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>;


public class PolyClipper : MonoBehaviour
{
    private static int precision = 1000;
    private static int ignoreArea = 600000;

    //START To separate global polygon helper class : ==========

    //Vector2[] -> List<IntPoint>
    private Path Vector2ToIntList(Vector2[] vertices)
    {
        Path path = new Path(vertices.Length);
        for (int i = 0; i < vertices.Length; i++)
        {
            path.Add(new IntPoint(FloatToInt(vertices[i].x),
                           FloatToInt(vertices[i].y)));
        }
        return path;
    }

    //List<IntPoint> -> Vector2[] 
    private Vector2[] IntListToVector2(Path path)
    {
        Vector2[] vertices = new Vector2[path.Count];
        for (int i = 0; i < path.Count; i++)
        {
            vertices[i] = new Vector2(IntToFloat((int)path[i].X),
                           IntToFloat((int)path[i].Y));
        }
        return vertices;
    }

    private List<Vector2[]> IntListsToVector2(Paths paths)
    {
        List<Vector2[]> verticesList = new List<Vector2[]>(paths.Count);
        for (int i = 0; i < paths.Count; i++)
        {
            if (Mathf.Abs((float)Clipper.Area(paths[i])) > ignoreArea)
            {
                verticesList.Add(IntListToVector2(paths[i]));
            }
        }
        return verticesList;
    }

    //Int -> Float
    private float IntToFloat(int input)
    {
        return input / (float)precision;
    }

    //Float -> Int 
    private int FloatToInt(float input)
    {
        return (int)Mathf.Floor(input * precision);
    }
    //END To separate global polygon helper class : ==========

    //Apply a polygon clipper operation on subject vertices using cut vertices
    public List<Vector2[]> ClipPoly(Vector2[] subject, Vector2[] cut, ClipType operation)
    {
        List<Vector2[]> cutPolygons = new List<Vector2[]>();

        Paths subj = new Paths(1);
        subj.Add(Vector2ToIntList(subject));

        Paths clip = new Paths(1);
        clip.Add(Vector2ToIntList(cut));

        Paths solution = new Paths();

        Clipper c = new Clipper();
        c.AddPaths(subj, PolyType.ptSubject, true);
        c.AddPaths(clip, PolyType.ptClip, true);

        c.Execute(operation, solution,
              PolyFillType.pftEvenOdd, PolyFillType.pftEvenOdd);

        return IntListsToVector2(solution);
    }

    public List<Vector2[]> SimplifyPolys(List<Vector2[]> polygons)
    {
        Paths paths = new Paths();
        for (int i = 0; i < polygons.Count; i++)
        {
            paths.Add(Vector2ToIntList(polygons[i]));
        }

        return IntListsToVector2(ClipperLib.Clipper.SimplifyPolygons(paths));
    }

}

