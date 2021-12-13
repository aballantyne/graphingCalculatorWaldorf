using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class controller : MonoBehaviour
{
    const int TOTAL_POINTS = 64; 
    const float DISTANCE_START = 100.0f; 

    public string input = "y=x*x";

    public float distance = 100.0f; 
    public float scaleY = 1.0f;
    LineRenderer lineRenderer;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        RenderLine();
    }

    // Update is called once per frame
    void Update( )
    {
        if (Input.mouseScrollDelta.y != 0 ){
            distance += Input.mouseScrollDelta.y; 
            if (distance<0.1f)distance = 0.1f;
            scaleY = DISTANCE_START /distance; 
            RenderLine();
        }
    }
    static float Eval(String expression)
    {
        System.Data.DataTable table = new System.Data.DataTable();
        return Convert.ToSingle(table.Compute(expression, String.Empty));
    }
    void RenderLine(){

        lineRenderer.positionCount = TOTAL_POINTS;
        lineRenderer.SetPositions(CalculateLine());

    }
    Vector3[] CalculateLine(){

        string str = input.Split('=')[1];

        Vector3[] points = new Vector3[TOTAL_POINTS]; 

        float newX = -distance/2f; 
        float increment = distance/TOTAL_POINTS;

        for (int i = 0; i < TOTAL_POINTS; i++){
            float newY = Eval(str.Replace("x",  Convert.ToString(newX))); 
            points[i] = new Vector3(newX,newY);
            newX+= increment;
        }
        for (int i = 0; i < TOTAL_POINTS; i++){
            
            points[i] *= scaleY;
            
        }
        return points;
    }
}
