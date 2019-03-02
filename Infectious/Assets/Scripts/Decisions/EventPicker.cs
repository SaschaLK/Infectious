using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPicker : MonoBehaviour
{

    ArrayList eventList = new ArrayList();
    private int i;
    public Text text;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickEvent() 
    {
        i = Random.Range(0, eventList.Count);
        Debug.Log(i);
        text.text = eventList[i].ToString();
    }

}
