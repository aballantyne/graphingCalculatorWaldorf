using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 
using Common.Utility;
using Math=Common.Utility.Math;

public class equation : MonoBehaviour
{
    const int TOTAL_POINTS = 64; 

    public static float scaleY = 1.0f;
    public static float distance = 100.0f; 

    LineRenderer lineRenderer;

    string INPUT = ""; 

    public GameObject uiController; 

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = TOTAL_POINTS;
    }
    public void UpdateInput(){
        uiController.GetComponent<equationUI>().UpdateInput();
    }
    public void RenderLine(){ 
        RenderLine(INPUT);
    }
    public void RenderLine(string input){
        
        INPUT = input;
        if (INPUT != String.Empty)
        lineRenderer.SetPositions(CalculateLine(input));
    }
    Vector3[] CalculateLine(string input){

        Vector3[] points = new Vector3[TOTAL_POINTS]; 

        float newX = -distance/2f; 
        float increment = distance/TOTAL_POINTS;

        for (int i = 0; i < TOTAL_POINTS; i++){
            string newEquation = input;
            Debug.Log(input);
            newEquation = newEquation.Replace("x",Convert.ToString(newX));
            float newY = Math.Eval(newEquation); 
            points[i] = new Vector3(newX,newY);
            newX+= increment;
        }
        for (int i = 0; i < TOTAL_POINTS; i++){
            
            points[i] *= scaleY;
            
        }
        return points; 
    }
    public void ClearLine(){
        Vector3[] points = new Vector3[TOTAL_POINTS];
        for (int i = 0; i < TOTAL_POINTS; i++){
            
            points[i] = new Vector3(0,0,0);
            
        }
        lineRenderer.SetPositions(points);
    }
    
}
