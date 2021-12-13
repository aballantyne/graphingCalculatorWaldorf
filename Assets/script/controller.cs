using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class controller : MonoBehaviour
{
    const int TOTAL_POINTS = 64; 
    string input = "y=x";

    public float distance = 20; 

    LineRenderer lineRenderer;
    // Start is called before the first frame update
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
            if (distance<0)distance = 0.1f;
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

        float x = -distance/2f; 
        float increment = distance/TOTAL_POINTS;

        for (int i = 0; i < TOTAL_POINTS; i++){
            float newX = i/3.2f - 10 ;
            float newY = Eval(str.Replace("x",  Convert.ToString(x))); 
            points[i] = new Vector3(newX,newY);
            x+= increment;
        }
        return points;
    }
}
