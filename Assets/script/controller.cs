using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class controller : MonoBehaviour
{
    const int TOTAL_POINTS = 64; 
    const float DISTANCE_START = 100.0f; 

    public float distance = 100.0f; 
    public float scaleY = 1.0f;

    public LinkedList<GameObject> equationList = new LinkedList<GameObject>();
    public LinkedList<GameObject> variableList = new LinkedList<GameObject>();
    
    public LinkedList<GameObject> itemList = new LinkedList<GameObject>();
    public int index = 0;

    public GameObject equationUI; 
    public GameObject equationOBJ;
    public GameObject variableUI; 

    public Transform canvas; 

    void Update(){
        if (Input.mouseScrollDelta.y != 0 ){
            distance += Input.mouseScrollDelta.y; 
            if (distance<0.1f)distance = 0.1f;
            scaleY = DISTANCE_START /distance; 
            equation.scaleY = scaleY;
            equation.distance = distance;
            
            UpdateRender(); 
        }
    }
    public void UpdateRender(){
        foreach (GameObject obj in equationList)
            {
                if (obj != null)
                    obj.GetComponent<equation>().UpdateInput(); 
            }
    }
    void CreateEquation(){
        int indexUI = itemList.Count; 
        int indexObj = equationList.Count; 
        GameObject newObj = Instantiate(equationOBJ, new Vector3(0, 0, 0), Quaternion.identity);
        newObj.name = String.Format("equationGameObject({0})",index);

        GameObject newUI = Instantiate(equationUI, canvas);
        itemList.AddLast(newUI);
        newUI.transform.SetParent(canvas);
        newUI.transform.localPosition = new Vector2(-20, 180 - (80 * index));
        equationList.AddLast(newObj); 

        newUI.name = String.Format("equationUI({0})",index);
        newUI.GetComponent<equationUI>().obj = newObj; 
        newUI.GetComponent<equationUI>().controller = gameObject; 
        newUI.GetComponent<equationUI>().indexUI = indexUI;
        newUI.GetComponent<equationUI>().indexObj = indexObj;  

        newObj.GetComponent<equation>().uiController = newUI;
        index++;
    }
    void  CreateVariable(){
        GameObject newUI = Instantiate(variableUI, canvas);

        newUI.transform.SetParent(canvas);
        newUI.name = String.Format("variableUI({0})",index);
        newUI.transform.localPosition = new Vector2(0, 170 - (80 * index));
        newUI.GetComponent<variableUI>().controller = gameObject; 
        newUI.GetComponent<variableUI>().isFirst = index == 0;

        itemList.AddLast(newUI); 
        index++;
    }
    public void UpdatePositions() { 
        
        UpdatePositions(false);
    }
    public void UpdatePositions(bool removeFirst){
        LinkedList<GameObject> newEquationList = new LinkedList<GameObject>();
        LinkedList<GameObject> newVariableList = new LinkedList<GameObject>();
        LinkedList<GameObject> newItemList = new LinkedList<GameObject>();

        int i = 0; 
        foreach (GameObject obj in itemList)
        {
            if (obj != null || removeFirst){
                removeFirst= false; 
                if (obj.GetComponent<equationUI>() != null){
                    newEquationList.AddLast(obj.GetComponent<equationUI>().obj);
                    if (i != 0 ){
                        obj.transform.localPosition = new Vector2(-20, 180 - (80 * (i-1)));
                    }else {
                        obj.transform.localPosition = new Vector2(-20, 180 - (80 * (i)));

                    }
                }
                if (obj.GetComponent<variableUI>() != null) {
                    if (i != 0 ){
                        obj.transform.localPosition = new Vector2(0, 180 - (80 * (i-1)));
                    }else {
                        obj.transform.localPosition = new Vector2(0, 180 - (80 * (i)));

                    }
                    
                }
                newItemList.AddLast(obj);
                i++;
            }
        }
        index--;
        equationList = newEquationList;
        itemList = newItemList; 
        variableList = newVariableList;
    }
}
