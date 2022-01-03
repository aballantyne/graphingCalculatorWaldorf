using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 
using TMPro;
using System;
using Math=Common.Utility.Math;
using Common.Utility; 
public class equationUI : MonoBehaviour
{
    public int indexUI;
    public int indexObj;

    public GameObject obj; 

    TMP_InputField INPUTFIELD; 
    public GameObject colorObject;
    TMP_InputField colorInputField; 

    public GameObject controller; 
    // Start is called before the first frame update
    void Start()
    {
        INPUTFIELD = GetComponent<TMP_InputField>();
        INPUTFIELD.onValueChanged.AddListener(delegate {UpdateInput(); });

        colorInputField = colorObject.GetComponent<TMP_InputField>();
        colorInputField.onValueChanged.AddListener(delegate {UpdateColor(); });
        
    }
    void UpdateColor (){
        Color color;
        if ( ColorUtility.TryParseHtmlString( "#" + colorInputField.text, out color))
         {obj.GetComponent<LineRenderer>().SetColors(color,color); Debug.Log("The color is valid");} 
    }

    // Update is called once per frame
    public void UpdateInput()
    {
        if (Math.IsInputValid(Math.FixFloat(Format.RemoveWhitespace((INPUTFIELD.text))))) { 
            string Input = 
                Math.ReplaceVariables(
                Math.InsertAsterisk( 
                Math.FixFloat( 
                Format.RemoveWhitespace(INPUTFIELD.text.Split('=')[1])))); 

            obj.GetComponent<equation>().RenderLine(Input);
        }else {
            obj.GetComponent<equation>().ClearLine();
        } 
         
    }
    void DestroyGameObject(){
        Destroy(gameObject);
        Destroy(obj);
        controller.GetComponent<controller>().UpdatePositions(indexUI == 0);
    }
}
