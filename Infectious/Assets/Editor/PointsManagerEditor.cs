using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PointsManager))]
public class PointsManagerEditor : Editor{
    public override void OnInspectorGUI() {
        PointsManager pointsManager = (PointsManager)target;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name");
        EditorGUILayout.LabelField("Value");
        EditorGUILayout.EndHorizontal();

        for(int i = 0; i < pointsManager.pointTypes.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            pointsManager.pointTypes[i].name = EditorGUILayout.TextField(pointsManager.pointTypes[i].name);
            pointsManager.pointTypes[i].value = EditorGUILayout.IntField(pointsManager.pointTypes[i].value);
            if (GUILayout.Button("Delete")) {
                pointsManager.pointTypes.Remove(pointsManager.pointTypes[i]);
                break;
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Type")) {
            pointsManager.pointTypes.Add(new PointsManager.PointType());
        }
        else if(GUILayout.Button("Delete List")) {
            pointsManager.pointTypes.Clear();
        }
        EditorGUILayout.EndHorizontal();
    }
}
