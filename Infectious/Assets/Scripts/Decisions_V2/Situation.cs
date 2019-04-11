using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Situation", menuName = "Situation", order = 1)]
public class Situation : ScriptableObject{

    [TextArea]
    public string situationText;

    [System.Serializable]
    public struct Decision {
        public string decisionName;
        public List<int> values;
    }
    public List<Decision> decisions;

    private void Awake() {
        foreach(Decision decision in decisions) {


            //decision.values.Add(1);
            //decision.values[0].
            //decision.values.Add(10);
            //decision.values.Add(100);


        }
    }
}