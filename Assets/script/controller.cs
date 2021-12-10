using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class controller : MonoBehaviour
{
    string input = "y=1/9*x";

    public Vector2 postion = new Vector2(0,0); 
    public float zoom = 5; 

    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        function();
    }

    // Update is called once per frame
    void Update( )
    {
        if (Input.mouseScrollDelta.y < 0 && zoom >= 0 || Input.mouseScrollDelta.y > 0){
            zoom -=Input.mouseScrollDelta.y; 
            function();
        }
        if (zoom < 0) zoom = 0;
    }
    static float Eval(String expression)
    {
        System.Data.DataTable table = new System.Data.DataTable();
        return Convert.ToSingle(table.Compute(expression, String.Empty));
    }
    void function(){
        string[] side = input.Split('=');
        string str = side [1];

        Vector3[] points = new Vector3[32]; 

        float x = postion.x - zoom/2; 

        for (int i = 0; i < 32; i++){
            points[i] = new Vector3(x * zoom,Eval(str.Replace("x",  Convert.ToString(x)))*zoom);
            x+= Math.Abs(zoom/32f);
        }
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);

    }
}
