using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenRay : MonoBehaviour
{
    LineRenderer rend;
    EdgeCollider2D edgeCol;

    public List<Vector2> linePoints = new List<Vector2>();
    
    void Start()
    {
        rend = GetComponent<LineRenderer>();
        edgeCol = GetComponent<EdgeCollider2D>();
        
    }

    
    void Update()
    {
        linePoints[0] = rend.GetPosition(0);
        linePoints[1] = rend.GetPosition(1);
        edgeCol.SetPoints(linePoints);
    }
}
