using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Situation", menuName = "Situation", order = 1)]
public class Situation : ScriptableObject{

    [TextArea]
    public string situationText;

    [System.Serializable]
    public class Decision {
        public string decisionName;
        //[ExecuteAlways]
        public List<int> values;
    }
    public List<Decision> decisions;

    private void Awake() {
        foreach(int value in decisions[0].values){



            //hallo
            //decision.values.Add(1);
            //decision.values[0].
            //decision.values.Add(10);
            //decision.values.Add(100);



        }
    }
}