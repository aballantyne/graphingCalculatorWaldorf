using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 
using System;

namespace Common.Utility
{
    public class Format{
        public static char[] operations = {'+','-','*','/'};

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray()); 
        }
        public static bool ContainsArray(Array a, object val)
        {
            return Array.IndexOf(a, val) != -1;
        }
        public static bool ContainsList(List<string> a, string val){
            bool isContained = false; 
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] == val) isContained = true;
            }
            return isContained;
        }
    }
    public class Math{
        
        public static float Eval(String expression)
        {
        
            System.Data.DataTable table = new System.Data.DataTable();
            return Convert.ToSingle(table.Compute(expression, String.Empty));
        
        }
        public static bool IsInputValid(string input){
            return IsInputValid(input,true);
        }
        public static bool IsInputValid(string input, bool notVariable){
            bool isValid = true; 
            if (input.Contains('=')){
                input = Format.RemoveWhitespace(input);
                char[]  expression = input.Split('=')[1].ToCharArray(); 
                for (int i = 0; i < expression.Length; i++)
                {
                    if (!(Char.IsLetter(expression[i])) && !(Char.IsNumber(expression[i]))){ 
                        if (i == 0 || i == expression.Length-1|| !Format.ContainsArray(Format.operations,expression[i]) && expression[i] != '.') 
                            isValid = false;   
                    }
                    if (Char.IsLetter(expression[i])&& !Format.ContainsList(Math.variables.Keys.ToList(),Convert.ToString(expression[i])) && expression[i] != 'x' && expression[i] != 'y') isValid = false;
                    
                    if (i != 0 && Char.IsNumber(expression[i]) && Char.IsLetter(expression[i-1])) isValid = false;; 
                }

                if (input.Split('=')[1] == String.Empty) isValid = false; 
                if (input.Split('=')[0] != "y" && notVariable)isValid = false; 
                if (!notVariable && (input.Contains('x') || input.Contains('y')))isValid = false; 
            }else { 
                isValid = false;
            }
            return isValid; 
        }

        public static string InsertAsterisk(string Input){
        
            char[] array = Input.ToCharArray(); 
            
            List<int> foundVariables = new List<int>();

            for (int i = 1; i < array.Length; i++){ 
                if (Char.IsLetter(array[i])  || Format.ContainsList(Math.variables.Keys.ToList(),Convert.ToString(array[i]))){
                    if ((Char.IsLetter(array[i-1]) || Char.IsNumber(array[i-1]))) 
                    foundVariables.Add(i);
                }
            }
            for (int i = foundVariables.Count-1; i >= 0 ; i--)
            {
                Input = Input.Insert(foundVariables[i],"*"); 
            }  
            return Input;
        }
    
        public static string FixFloat(string input){
                //.
                //returns 0.0
                char[] array = input.ToCharArray(); 
            
                List<int> foundNumbers = new List<int>();

                for (int i = 0; i < array.Length; i++){ 
                    if (array[i] == '.'){
                        if (i == 0 || !(Char.IsNumber(array[i-1])))
                            foundNumbers.Add(i);
                        if (i == array.Length-1 || !(Char.IsNumber(array[i+1])))
                            foundNumbers.Add(i+1);
                    }
                }
                //3.x*.1
                //3.0x*0.1
                for (int i = foundNumbers.Count-1; i >= 0 ; i--)
                {
                    input = input.Insert(foundNumbers[i],"0"); 
                }  
                return input;
        }
        public static Dictionary<string,float> variables = new Dictionary<string,float>(); 

        public static string ReplaceVariables(string input){ 

            List<string> variableKey = Math.variables.Keys.ToList();
            foreach (string item in variableKey)
            {
                input = input.Replace(item,Convert.ToString(variables[item]));
            }
            return input; 
        }
    }
}