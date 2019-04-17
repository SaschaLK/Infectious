using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Situation", menuName = "Situation", order = 1)]
public class Situation : ScriptableObject{
    //Foreach type of pointsvalue add that type to each decision of the situation
    private void OnEnable() {
        for(int i = 0; i < decisions.Count; i++) {
            for(int k = 0; k < GameObject.FindObjectOfType<PointsManager>().pointTypes.Count; k++) {
                decisions[i].values.Add(0);
            }
        }
        for(int i = 0; i < GameObject.FindObjectOfType<PointsManager>().pointTypes.Count; i++) {
            minPointValues.Add(0);
        }
    }

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