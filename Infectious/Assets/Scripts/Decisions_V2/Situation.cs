using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Situation", menuName = "Situation", order = 1)]
public class Situation : ScriptableObject{
    //The parameters and variables for a decision
    public List<int> minPointValues;
    [TextArea]
    public string situationText;
    [System.Serializable]
    public class Decision {
        public string decisionName;
        public List<int> values;
    }

    //List containing all decisions for that specific situation
    public List<Decision> decisions;
}