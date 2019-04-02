using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClock : MonoBehaviour {

    private System.DateTime gameDay;
    public Text gameClock;
    

    
    void Start() 
    {

        gameDay = this.GetComponent<PointLog>().GameDate;
        
    }

    
    void Update()
    {

        //Das aktuelle Datum im Spiel wird aus dem PointLog Script abgerufen und zur Ausgabe als Text im UI Formatiert.
        gameDay = this.GetComponent<PointLog>().GameDate;
        gameClock.text = string.Format("{0}.{1}.{2}", gameDay.Day, gameDay.Month, gameDay.Year);
    }
}
