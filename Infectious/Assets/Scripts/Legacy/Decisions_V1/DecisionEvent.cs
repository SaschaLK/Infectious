using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionEvent : MonoBehaviour
{

    GameObject LM;
    public GameObject UICanvas;
    private float timer;


    
    void Start()
    {
        LM = this.gameObject;  
    }

    
    void Update()
    {


        //Es wird in regelmäßigen Abständen ein UI-Dialog  mit einem Event aktiviert.
        if (UICanvas.activeSelf == false) 
        {
            if (timer > 5.0f) 
            {
                UICanvas.SetActive(true);
                timer -= 5.0f;
            }
            timer += Time.deltaTime;

        }
        
    }

    //Das Event gibt zwei verschiedene Optionen zur Auswahl, die jeweils entweder den Wert(in PointLog) vergrößern oder verringern. Anschließend wird der Dialog wieder ausgeschaltet.

    public void Up() 
    {
        LM.GetComponent<PointLog>().AddValue();
        UICanvas.SetActive(false);
    }

    public void Down() 
    {
        LM.GetComponent<PointLog>().SubtractValue();
        UICanvas.SetActive(false);
    }
}

