using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines.Interpolators;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    private List<Vector2> points;
    [SerializeField] private RectTransform mRectTransform;
    [SerializeField] private float thickness = 10f;
    [SerializeField] private float lineWidth = 10f;
    private float alpha = 0f;

    [SerializeField] private Color leftColor = Color.white;
    [SerializeField] private Color rightColor = Color.black;

    //TODO: make the UI line render start from the end and move to the middle as before
    private void Update()
    {
        if (MusicManager.Instance == null || MusicManager.Instance.GetMusicPlayer() == null) { return; }
        if (!MusicManager.Instance.GetMusicPlayer().IsTickEnabled()) { return; }

        float progress = MusicManager.Instance.GetSampleProgress();
        float rectWidth = mRectTransform.rect.width + (lineWidth * 2);

        points = new List<Vector2>
        {
            new Vector2((rectWidth * progress) - rectWidth / 2 - lineWidth / 2, 0),
            new Vector2((rectWidth * progress) - rectWidth / 2 + lineWidth / 2, 0)
        };

        alpha = progress;


        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        Rect rect = mRectTransform.rect;

        //Gradient Background
        int startIdx = vh.currentVertCount;

        UIVertex vertex = UIVertex.simpleVert;

        vertex.position = new Vector3(rect.xMin, rect.yMin);
        vertex.color = leftColor;
        vh.AddVert(vertex);

        vertex.position = new Vector3(rect.xMin, rect.yMax);
        vertex.color = leftColor;
        vh.AddVert(vertex);

        vertex.position = new Vector3(rect.xMax, rect.yMax);
        vertex.color = rightColor;
        vh.AddVert(vertex);

        vertex.position = new Vector3(rect.xMax, rect.yMin);
        vertex.color = rightColor;
        vh.AddVert(vertex);

        vh.AddTriangle(startIdx, startIdx + 1, startIdx + 2);
        vh.AddTriangle(startIdx + 2, startIdx + 3, startIdx);

        // Line Renderer
        if (points == null || points.Count < 2)
            return;

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 p1 = points[i];
            Vector2 p2 = points[i + 1];

            // Clamp inside rect
            p1.x = Mathf.Clamp(p1.x, rect.xMin, rect.xMax);
            p2.x = Mathf.Clamp(p2.x, rect.xMin, rect.xMax);
            p1.y = Mathf.Clamp(p1.y, rect.yMin, rect.yMax);
            p2.y = Mathf.Clamp(p2.y, rect.yMin, rect.yMax);

            Vector2 direction = (p1 - p2).normalized;
            Vector2 normal = new Vector2(-direction.y, direction.x);

            int idx = vh.currentVertCount;

            vertex = UIVertex.simpleVert;
            vertex.color = new Color(color.r, color.g, color.b, alpha);

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
