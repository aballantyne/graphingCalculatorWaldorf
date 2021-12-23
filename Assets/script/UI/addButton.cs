using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class addButton : MonoBehaviour
{
    public GameObject createButtons; 

    private bool isClicked;  

    public bool visible = false; 
    // Start is called before the first frame update
    void Start()
    {
		GetComponent<Button>().onClick.AddListener(TaskOnClick);
        //if (true && false || true && false) {Debug.Log(":(");} else{Debug.Log(":)");}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !isClicked || visible && isClicked) {
            isClicked = false; 
            visible = false;
        }else if (Input.GetMouseButtonUp(0) && isClicked){   
            isClicked = false; 
            visible = true;
        } 
        createButtons.SetActive(visible); 
    }
    void TaskOnClick() {
        isClicked = true; 
    }
}
