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
    public string Input, output;
    public bool isValid; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        output = Math.ReplaceVariables(Math.InsertAsterisk(Input));
        isValid = Math.IsInputValid(output);
    }
}
