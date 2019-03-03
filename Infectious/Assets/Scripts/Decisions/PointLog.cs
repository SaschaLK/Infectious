using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLog : MonoBehaviour
{

    public static PointLog pointLog;


    private float Wert01 = 0.0f;                //Interne Variable für den Gesamtwert
    private System.DateTime gameDate;           //Interne Spieluhr
    private float timer = 0.0f;                 //Timer für die Spieluhr
    private bool rising = false;                //Variable zum ein- und ausschalten des Wachstumsbias

    public float gameSpeedPerDay = 5.0f;        //Public Variable zur Manipulation der Spielgeschwindigkeit im Inspector
    public int Steps;                           //Public Variable zum Bestimmen der Anzahl an benötigten Schritte zum einfärben eines Feldes im Inspector
    public float bias = 0;                      //Public Variable zur Manipulation des Wachstumsbias im Inspector

    

    public DateTime GameDate { get => gameDate; set => gameDate = value; }          //Public Variable zum Abrufen des aktuellen Spieltags
    public float Wert011 { get => Wert01; set => Wert01 = value; }                  //Public Variable zum Abrufen des Gesamtwerts

    private void Awake() {
        pointLog = this;
    }
    

    void Start()
    {
       
        // Der Starttag des Spiels wird auf den aktuellen Tag gesetzt.
        GameDate = System.DateTime.Today;

    }

    void Update()
    {
        
        //In regelmäßigen Abständen wird die Spieluhr um einen Tag nach vorne gesetzt.
        if (timer > gameSpeedPerDay) 
        {    
            timer = timer - gameSpeedPerDay;
            GameDate = GameDate.AddDays(1);

            //Bei aktiviertem Wachstumsbias steigt der Wert jeden Spieltag automatisch leicht an.
            if (rising == true) {
                Wert01 += bias;
            }

        }

        

        timer += Time.deltaTime;

    }

    public void AddValue() 
    {
        //Der Wert wird um eine Einheit hinaufgesetzt und der Wachstumsbias aktiviert
        Wert01 += (1.0f / Steps);
        rising = true;

    }

    public void SubtractValue() 
    {
        //Der Wert wird um eine Einheit herabgesetzt und der Wachstumsbias deaktiviert
        Wert01 -= (1.0f / Steps);
        rising = false;

    }

    
}
