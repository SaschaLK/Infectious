using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClock : MonoBehaviour {

    private System.DateTime gameDay;
    public Text gameClock;
    

    // Start is called before the first frame update
    void Start() 
    {
        gameDay = this.GetComponent<PointLog>().GameDate;
        
    }

    // Update is called once per frame
    void Update()
    {
        gameDay = this.GetComponent<PointLog>().GameDate;
        gameClock.text = string.Format("{0}.{1}.{2}", gameDay.Day, gameDay.Month, gameDay.Year);
    }
}
