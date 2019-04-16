using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Situation))]
public class SituationEditor : Editor{
    private List<PointsManager.PointType> pointTypes;

    //Finding the PointsManager for future Reference, such as knowing how many types of points exist.
    private void OnEnable() {
        pointTypes = GameObject.FindObjectOfType<PointsManager>().pointTypes;
    }

    public override void OnInspectorGUI() {
        Situation situation = (Situation)target;

        //Description and flavor text of the situation
        EditorGUILayout.LabelField("Situation Flavor Text");
        situation.situationText = EditorGUILayout.TextArea(situation.situationText);
        EditorGUILayout.Space();

        //List of the possible decisions for this specific situation
        EditorGUILayout.LabelField("Decisions");
        EditorGUI.indentLevel++;
        for (int i = 0; i < situation.decisions.Count; i++) {
            //Decision name and delete button
            EditorGUILayout.BeginHorizontal();
            situation.decisions[i].decisionName = EditorGUILayout.TextField(situation.decisions[i].decisionName);
            if (GUILayout.Button("Delete")) {
                situation.decisions.Remove(situation.decisions[i]);
                break;
            }
            EditorGUILayout.EndHorizontal();

            //The influence of the decision. The types of points are fetched from the pointsManager and the Designer
            //can change the impact of every decision on the point total
            EditorGUI.indentLevel++;
            for(int k = 0; k < pointTypes.Count; k++) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(pointTypes[k].name);
                situation.decisions[i].values[k] = EditorGUILayout.IntField(situation.decisions[i].values[k]);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }
        EditorGUI.indentLevel--;

        //List management functions
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("New Decision")) {
            situation.decisions.Add(new Situation.Decision());
        }
        else if(GUILayout.Button("Delete List")) {
            situation.decisions.Clear();
        }
        EditorGUILayout.EndHorizontal();

        //Maybe need to add: if gui.changed -> editorutility.setdirty(situation);
    }
}
