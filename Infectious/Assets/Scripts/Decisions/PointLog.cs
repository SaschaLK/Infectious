using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLog : MonoBehaviour
{

    public static PointLog pointLog;


    private float Wert01 = 0.0f;
    
    public float baseTick = 1.1f;
    public float tick = 5.0f;
    private float timer = 0.0f;
    private System.DateTime gameDate;
    public int Steps;
    private bool rising = false;
    public float bias = 0;

    GameObject LM;

    public DateTime GameDate { get => gameDate; set => gameDate = value; }
    public float Wert011 { get => Wert01; set => Wert01 = value; }

    private void Awake() {
        pointLog = this;
    }

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
        //ColorChanger.colorChanger.ProgressUpdate(Wert01);
        
        timer += Time.deltaTime;
        if (timer > tick) 
        {
            
            timer = timer - tick;
            GameDate = GameDate.AddDays(1);
            Debug.Log(Wert01);
            
        }
        if (rising == true) {
            Wert01 += bias;
        }

       
    }

    public void AddValue() 
    {
        
        Wert01 += (1.0f / Steps);
        
        rising = true;
    }

    public void SubtractValue() 
    {
        
        Wert01 -= (1.0f / Steps);
        rising = false;
    }

    
}
