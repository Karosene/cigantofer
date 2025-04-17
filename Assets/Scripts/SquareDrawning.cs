using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct Stvorec
{
    public float X;
    public float Y;
    public float W;
    public float H;
}

[Serializable]
public struct Kruh
{
    public float X;
    public float Y;
    public float Radius;
}


public class SquareDrawning : MonoBehaviour
{
    public Stvorec A;
    public Kruh B;
    public Kruh C;

    private void Update()
    {
 // Draw the square
        Vector2 posSquare = new Vector2(A.X, A.Y);
        Vector2 sizeSquare = new Vector2(A.W, A.H);
        DrawSquare(sizeSquare, posSquare, Color.black);

        Vector2 centerCircleB = new Vector2(B.X, B.Y);
        DrawCircle(centerCircleB, B.Radius, Color.blue);

        Vector2 centerCircleC = new Vector2(C.X, C.Y);
        DrawCircle(centerCircleC, C.Radius, Color.red);

        // Optional: Draw red line if they intersect (simple AABB check for square and bounding box of circle)
        Rect squareRect = new Rect(A.X, A.Y, A.W, A.H);
        Rect circleBoundingBox = new Rect(B.X - B.Radius, B.Y - B.Radius, B.Radius * 2, B.Radius * 2);
        
        //if  ((A.X + A.W > B.X) && (B.Y + B.H > A.Y) && (B.X + B.W > A.X) && (A.Y + A.H > B.Y))
        // {
        //     Debug.DrawLine(_pos1 + new Vector2(_size1.x / 2, _size1.y / 2),
        //         _pos2 + new Vector2(_size2.x / 2, _size2.y / 2), Color.red);
        // }

        if  (((B.X - C.X) * (B.X - C.X) + ((B.Y - C.Y) * (B.Y - C.Y))) < (B.Radius + C.Radius) * (B.Radius + C.Radius))
        {
            Debug.DrawLine(centerCircleC, centerCircleB, Color.red);
        }
    }
    private void DrawSquare(Vector2 size, Vector2 pos, Color color)
    {
        Vector3 bottomLeft = transform.position + (Vector3)pos;
        Vector3 bottomRight = bottomLeft + new Vector3(size.x, 0, 0);
        Vector3 topLeft = bottomLeft + new Vector3(0, size.y, 0);
        Vector3 topRight = bottomLeft + new Vector3(size.x, size.y, 0);

        Debug.DrawLine(bottomLeft, bottomRight, color);
        Debug.DrawLine(bottomLeft, topLeft, color);
        Debug.DrawLine(topLeft, topRight, color);
        Debug.DrawLine(bottomRight, topRight, color);
    }

    private void DrawCircle(Vector2 center, float radius, Color color, int segments = 36)
    {
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector2(radius, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Debug.DrawLine(transform.position + prevPoint, transform.position + newPoint, color);
            prevPoint = newPoint;
        }
    }
}
