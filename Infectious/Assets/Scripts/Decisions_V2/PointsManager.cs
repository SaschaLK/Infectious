using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour {
    public static PointsManager instance;

    [System.Serializable]
    public class PointType {
        public string name;
        public int value;
    }
    public List<PointType> pointTypes = new List<PointType>();

    private void Awake() {
        instance = this;
    }
}