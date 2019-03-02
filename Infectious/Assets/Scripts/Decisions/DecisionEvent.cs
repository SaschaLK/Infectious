using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionEvent : MonoBehaviour
{

    GameObject LM;
    public GameObject UICanvas;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        LM = this.gameObject;  
    }

    // Update is called once per frame
    void Update()
    {
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

