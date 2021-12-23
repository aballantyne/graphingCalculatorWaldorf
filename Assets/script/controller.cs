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
                    obj.GetComponent<equation>().RenderLine(); 
            }
    }
    void CreateEquation(){
        int indexUI = itemList.Count; 
        int indexObj = equationList.Count; 
        GameObject newObj = Instantiate(equationOBJ, new Vector3(0, 0, 0), Quaternion.identity);
        equationList.AddLast(newObj); 
        newObj.name = String.Format("equationGameObject({0})",index);

        GameObject newUI = Instantiate(equationUI, canvas);
        itemList.AddLast(newUI);

        newUI.transform.SetParent(canvas);
        newUI.name = String.Format("equationUI({0})",index);
        newUI.transform.position = new Vector2(97.5f, 400 - (80 * index));
        newUI.GetComponent<equationUI>().obj = newObj; 
        newUI.GetComponent<equationUI>().controller = gameObject; 
        newUI.GetComponent<equationUI>().indexUI = indexUI;
        newUI.GetComponent<equationUI>().indexObj = indexObj;  

        index++;
    }
    void CreateVariable(){
        GameObject newUI = Instantiate(variableUI, canvas);

        newUI.transform.SetParent(canvas);
        newUI.name = String.Format("variableUI({0})",index);
        newUI.transform.position = new Vector2(148, 400 - (80 * index));
        index++;
    }
    public void UpdatePositions() { 
        Debug.Log(false);
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
                obj.transform.position = new Vector2(97.5f, 400 - (80 * (i-1)));
                if (obj.GetComponent<equationUI>() != null){
                    newEquationList.AddLast(obj.GetComponent<equationUI>().obj);
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
