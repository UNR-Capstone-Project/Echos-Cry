using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    public List<Vector2> points;
    public float thickness = 10f;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (points.Count < 2)
        {
            Debug.Log("A line must consist of at least two points!");
            return;
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 p1 = points[i];
            Vector2 p2 = points[i + 1];

            Vector2 direction = (p1 - p2).normalized;
            Vector2 normal = new Vector2(-direction.y, direction.x);

            int idx = vh.currentVertCount;

            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            vertex.position = p1 + normal * (thickness / 2);
            vh.AddVert(vertex);

            vertex.position = p1 - normal * (thickness / 2);
            vh.AddVert(vertex);

            vertex.position = p2 - normal * (thickness / 2);
            vh.AddVert(vertex);

            vertex.position = p2 + normal * (thickness / 2);
            vh.AddVert(vertex);

            vh.AddTriangle(idx, idx + 1, idx + 2);
            vh.AddTriangle(idx + 2, idx + 3, idx);
        }
    }
}
