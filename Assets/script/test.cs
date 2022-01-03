using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using System;
using Unity.Mathematics; 
using System.Globalization;
using Common.Utility; 
using Math=Common.Utility.Math;

public class test : MonoBehaviour
{
    public string Input = "y=";
    public float output;
    public bool isValid; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        output = Math.Eval(Math.ReplaceVariables(
                Math.InsertAsterisk( 
                Math.FixFloat( 
                Format.RemoveWhitespace(Input.Split('=')[1])))));
        isValid = Math.IsInputValid(Math.ReplaceVariables(Math.InsertAsterisk(Input)));
    }
}
