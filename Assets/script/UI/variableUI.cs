using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using TMPro;
using System;
using Common.Utility; 
using Math=Common.Utility.Math;
using System.Text.RegularExpressions;
public class variableUI : MonoBehaviour
{
    TMP_InputField INPUTFIELD; 

    public float value;

    public string letter; 

    public GameObject controller;

    public bool isFirst = true;  
    // Start is called before the first frame update
    void Start()
    {
        INPUTFIELD = GetComponent<TMP_InputField>();
        INPUTFIELD.onValueChanged.AddListener(delegate {UpdateInput(); });
    }

    // Update is called once per frame
    void UpdateInput()
    {
        if (Math.IsInputValid(Math.FixFloat(INPUTFIELD.text),false) && Regex.IsMatch(Format.RemoveWhitespace(INPUTFIELD.text.Split('=')[0]), @"^[a-zA-Z]+$") && INPUTFIELD.text.Split('=')[0].Length == 1) {
            value = 
                Math.Eval(
                Math.ReplaceVariables(
                Math.InsertAsterisk( 
                Math.FixFloat( 
                Format.RemoveWhitespace(INPUTFIELD.text.Split('=')[1]))))); 
            if (letter != Format.RemoveWhitespace(INPUTFIELD.text.Split('=')[0])){
                if (Math.variables.ContainsKey(letter)) Math.variables.Remove(letter);

                letter = Format.RemoveWhitespace(INPUTFIELD.text.Split('=')[0]);
            }
             if (!Math.variables.ContainsKey(letter)){
                Math.variables.Add (letter,value);
            }else {
                Math.variables.Remove(letter);
                Math.variables.Add (letter,value);

            }
            controller.GetComponent<controller>().UpdateRender();
        }
        
    }
    void DestroyGameObject(){
        Math.variables.Remove(letter);
        Destroy(gameObject);
        controller.GetComponent<controller>().UpdatePositions();
        controller.GetComponent<controller>().UpdateRender();
    }
    
}
