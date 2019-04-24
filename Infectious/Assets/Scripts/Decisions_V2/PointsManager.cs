using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour {
    #region Setup
    public static PointsManager instance;

    //Variables
    public int tendencyImpactDevider;
    [System.Serializable]
    public class PointType {
        public string name;
        public int score;
        public float currentScoreFloat;
        public float currentScorePercentage;
        public float nullPoint;
        public float medianPoint;
        public float doublePoint;
        public float tendency;
    }
    public List<PointType> pointTypes = new List<PointType>();
    public float unifiedPointsFactor;

    //Setup Calls
    private void Awake() {
        instance = this;
    }

    private void Start() {
        PassPoints();
        StartCoroutine(UpdatePointsLoop());
    }

    private void PassPoints() {
        foreach(PointType point in pointTypes) {
            point.currentScoreFloat = point.score;
            point.currentScorePercentage = point.score / point.medianPoint;
        }
    }
    #endregion

    #region Update Points
    //Updates points in intervalls
    private IEnumerator UpdatePointsLoop() {
        while (true) {
            yield return new WaitForFixedUpdate();
            unifiedPointsFactor = 0;
            foreach(PointType point in pointTypes) {
                if (point.currentScoreFloat == 0 && point.tendency > 0) {
                    point.currentScoreFloat = 0.01f;
                }
                else if (point.currentScoreFloat == 0 && point.tendency < 0) {
                    point.currentScoreFloat = -0.01f;
                }
                point.currentScoreFloat += point.currentScoreFloat * (point.tendency/tendencyImpactDevider);
                point.currentScorePercentage = point.currentScoreFloat / point.medianPoint;
                unifiedPointsFactor += point.currentScorePercentage;
                point.score = (int)point.currentScoreFloat;
            }
            unifiedPointsFactor = unifiedPointsFactor / pointTypes.Count;
        }
    }

    //Called once when a Decision or Event happens
    public void UpdatePointsWithDecision(Situation.Decision dec) {
        for(int i = 0; i < pointTypes.Count; i++) {
            pointTypes[i].currentScoreFloat += dec.values[i];
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
        StartCoroutine(UpdatePointsLoop());
    }
    #endregion
}