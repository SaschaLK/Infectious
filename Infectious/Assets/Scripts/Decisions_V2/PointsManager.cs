using System.Collections;
using System.Collections.Generic;
using System.Reflection;
//using System.ComponentModel.Design;
using UnityEngine;
using UnityEditor;

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