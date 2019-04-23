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
        StartCoroutine(UpdatePoints());
    }

    private void PassPoints() {
        foreach(PointType point in pointTypes) {
            point.currentScore = point.score;
        }
    }
    #endregion

    #region Update Points
    private IEnumerator UpdatePoints() {
        while (true) {
            yield return new WaitForSecondsRealtime(updateTime);
            foreach(PointType point in pointTypes) {
                point.currentScore += point.currentScore * point.tendency;
                point.score = (int)point.currentScore;
            }
        }
    }
    #endregion
}