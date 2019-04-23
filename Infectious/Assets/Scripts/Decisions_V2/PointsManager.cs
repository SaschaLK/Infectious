using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour {
    #region Setup
    public static PointsManager instance;

    //Variables
    public float updateTime;
    [System.Serializable]
    public class PointType {
        public string name;
        public int score;
        public float currentScore;
        public float tendency;
    }
    public List<PointType> pointTypes = new List<PointType>();

    //Setup Calls
    private void Awake() {
        instance = this;
    }

    private void Start() {
        PassPoints();
        StartCoroutine(PointUpdateLoop());
    }

    private void PassPoints() {
        foreach(PointType point in pointTypes) {
            point.currentScore = point.score;
        }
    }
    #endregion

    #region Update Points
    private IEnumerator PointUpdateLoop() {
        while (true) {
            yield return new WaitForSecondsRealtime(updateTime);
            foreach(PointType point in pointTypes) {
                if (point.currentScore == 0 && point.tendency > 0) {
                    point.currentScore = 0.1f;
                }
                else if (point.currentScore == 0 && point.tendency < 0) {
                    point.currentScore = -0.1f;
                }
                point.currentScore += point.currentScore * point.tendency;
                point.score = (int)point.currentScore;
            }
        }
    }

    public void UpdatePoints(Situation.Decision dec) {
        for(int i = 0; i < pointTypes.Count; i++) {
            pointTypes[i].currentScore += dec.values[i];
            if(pointTypes[i].tendency + dec.tendencies[i] > 1) {
                pointTypes[i].tendency = 1;
            }
            else if(pointTypes[i].tendency + dec.tendencies[i] < -1) {
                pointTypes[i].tendency = -1;
            }
            else {
                pointTypes[i].tendency += dec.tendencies[i];
            }
        }
        StartCoroutine(PointUpdateLoop());
    }
    #endregion
}