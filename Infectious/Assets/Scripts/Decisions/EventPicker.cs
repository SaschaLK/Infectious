using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPicker : MonoBehaviour
{

    /*
     * 
     * AKTUELL GRÖßTENTEILS PLATZHALTER
     * 
     * 
     * 
    */


    ArrayList eventList = new ArrayList();
    private int i;
    public Text text;
    
    //Die Klasse füllt zu Beginn ein Array mit Events.
    void Start()
    {
        eventList.Add("Event1");
        eventList.Add("Event2");
        eventList.Add("Event3");
        eventList.Add("Event4");
        eventList.Add("Event5");
        eventList.Add("Event6");
        eventList.Add("Event7");
        eventList.Add("Event8");
        eventList.Add("Event9");
        eventList.Add("Event10");
    }

    
    void Update()
    {
        
    }

    //Es wird ein zufälliges Event aus dem Array gewählt und UI als Text angezeigt.
    public void PickEvent() 
    {
        i = Random.Range(0, eventList.Count);
        Debug.Log(i);
        text.text = eventList[i].ToString();
    }

}
