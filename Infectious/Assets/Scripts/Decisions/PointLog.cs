using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLog : MonoBehaviour
{
    
    private float Wert01 = 0.0f;
    private float tickrateCURRENT = 1.0f;
    private float tickrateNEW = 1.1f;
    public float baseTick = 1.1f;
    public float tick = 5.0f;
    private float timer = 0.0f;
    private System.DateTime gameDate;
    public int Steps;
    private bool rising = false;
    public float bias = 0.0001f;

    GameObject LM;

    public DateTime GameDate { get => gameDate; set => gameDate = value; }


    // Start is called before the first frame update
    void Start()
    {
        
        LM = this.gameObject;
        
        //tickrateNEW = baseTick;
        
        GameDate = System.DateTime.Today;

    }

    // Update is called once per frame
    void Update()
    {
        ColorChanger.colorChanger.ProgressUpdate(Wert01);
        
        timer += Time.deltaTime;
        if (timer > tick) 
        {
            //Wert01 = Wert01 + tickrateNEW;
            //tickrateCURRENT = tickrateNEW;
            timer = timer - tick;
            GameDate = GameDate.AddDays(1);
            Debug.Log(Wert01);
            //Debug.Log(tickrateNEW);
            //Debug.Log(baseTick);
        }
        if (rising == true) {
            Wert01 += bias;
        }

       
    }

    public void AddValue() 
    {
        //tickrateNEW = tickrateCURRENT * 1.1f;
        Wert01 += (1.0f / Steps);
        ColorChanger.colorChanger.ResetTimer();
        //ColorChanger.colorChanger.ProgressUpdate(Wert01);
        rising = true;
    }

    public void SubtractValue() 
    {
        //tickrateNEW = tickrateCURRENT * 0.9f;
        Wert01 -= (1.0f / Steps);
        ColorChanger.colorChanger.ProgressUpdate(Wert01);
    }

    
}
