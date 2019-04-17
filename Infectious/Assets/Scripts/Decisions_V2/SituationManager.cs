using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour {

    public static SituationManager instance;

    public float tempTimeDelay;
    public List<Situation> situations = new List<Situation>();

    private float tempTimeStamp;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        tempTimeStamp = tempTimeDelay;
    }

    private void Update() {
        tempTimeDelay -= Time.deltaTime;
        if(tempTimeDelay <= 0) {
            //tempTimeDelay = tempTimeStamp;
            Time.timeScale = 0;
            Debug.Log(Time.time);
        }
    }
}
