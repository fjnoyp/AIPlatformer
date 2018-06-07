using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonToSpringStructure : MonoBehaviour
{
    public GameObject circleTemplate;
    private List<AnchoredJoint2D> debugJoints = new List<AnchoredJoint2D>(); 

    // Use this for initialization
    void Start()
    {
        //Should work solely in local space 
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        Bounds bounds = collider.bounds;

        float spacing = 4;
        float halfSpacing = spacing / 2;

        Vector2 curPos = new Vector2(bounds.min.x + halfSpacing, bounds.min.y + halfSpacing);

        GameObject[,] circles = new GameObject[(int)Mathf.Ceil(bounds.size.x)+1, (int)Mathf.Ceil(bounds.size.y)+1];

        int xIndex = 0;
        int yIndex = 0;

        while (curPos.y < bounds.max.y)
        {
            curPos.x = bounds.min.x + halfSpacing;
            xIndex = 0;
            while (curPos.x < bounds.max.x)
            {
                if (collider.OverlapPoint(curPos))
                {
                    GameObject circle = GameObject.Instantiate(circleTemplate);
                    //circle.transform.parent = this.transform;
                    circle.transform.position = curPos;
                    circle.transform.localScale = new Vector2(.25f, .25f);

                    circles[xIndex, yIndex] = circle;
                }
                curPos.x += spacing;
                xIndex++;
            }
            curPos.y += spacing;
            yIndex++;
        }

        for (int curX = 0; curX < circles.GetLength(0) - 1; curX++)
        {
            for (int curY = 0; curY < circles.GetLength(1) - 1; curY++)
            {
                if (circles[curX, curY] != null)
                {
                    GameObject source = circles[curX, curY]; 

                    createJoint(source, circles[curX + 1, curY]);
                    createJoint(source, circles[curX + 1, curY + 1]);
                    createJoint(source, circles[curX, curY + 1]); 
                    if(curY > 0) createJoint(source, circles[curX + 1, curY - 1]); 
                }
            }
        }

    }

    private FixedJoint2D createJoint(GameObject source, GameObject dest)
    {
        if (source == null || dest == null) return null; 

        FixedJoint2D joint = source.AddComponent<FixedJoint2D>();
        joint.connectedBody = dest.GetComponent<Rigidbody2D>();
        joint.enableCollision = true;
        joint.dampingRatio = 1;

        drawLine(source.transform.position, dest.transform.position);
        debugJoints.Add(joint); 

        return joint; 
    }


    public void Update()
    {
        for(int i = 0; i<debugJoints.Count; i++)
        {
            Debug.DrawLine(debugJoints[i].gameObject.transform.position, debugJoints[i].connectedBody.transform.position); 
        }
    }

    void drawLine(Vector2 start, Vector2 end)
    {
        float angle = Mathf.Rad2Deg * Mathf.Atan2(end.y - start.y, end.x - start.x);

        float dist = Mathf.Abs((end - start).magnitude);

        GameObject line = GameObject.CreatePrimitive(PrimitiveType.Cube);

        line.transform.position = new Vector2((start.x + end.x) / 2, (start.y + end.y) / 2);
        line.transform.localScale = new Vector2(dist, .1f);
        line.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
