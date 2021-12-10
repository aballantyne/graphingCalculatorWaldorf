using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class controller : MonoBehaviour
{
    string input = "y=4*x+1";

    Vector2 postion = new Vector2(0,0); 
    float zoom = 10; 

    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update( )
    {
        if (Input.mouseScrollDelta.y != 0){
            zoom +=Input.mouseScrollDelta.y; 
            Camera.main.orthographicSize= zoom;  
            function();
        }
    }
    static Double Eval(String expression)
    {
        System.Data.DataTable table = new System.Data.DataTable();
        return Convert.ToDouble(table.Compute(expression, String.Empty));
    }
    void function(){
        string[] side = input.Split('=');
        string str = side [1];

        lineRenderer = GetComponent<LineRenderer>();
        Vector3[] points = new Vector3[zoom];
        int index = 0;
     
        for (float x = postion.x-zoom; x < postion.x + 10; x++){
            points[index] = new Vector3(x,Convert.ToSingle(Eval(str.Replace("x",  Convert.ToString(x)))));
            index++;
        }
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);

    }
}
