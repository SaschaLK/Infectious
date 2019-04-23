using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PointsManager))]
public class PointsManagerEditor : Editor{
    public override void OnInspectorGUI() {
        PointsManager pointsManager = (PointsManager)target;

        //Column labels
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Type Name");
        GUILayout.Label("Score");
        GUILayout.Label("Tendency");
        EditorGUILayout.EndHorizontal();

        //List for point-types
        for(int i = 0; i < pointsManager.pointTypes.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            pointsManager.pointTypes[i].name = EditorGUILayout.TextField(pointsManager.pointTypes[i].name);
            pointsManager.pointTypes[i].score = EditorGUILayout.IntField(pointsManager.pointTypes[i].score);
            pointsManager.pointTypes[i].tendency = EditorGUILayout.Slider(pointsManager.pointTypes[i].tendency, -1, 1);

            if (GUILayout.Button("Delete")) {
                pointsManager.pointTypes.Remove(pointsManager.pointTypes[i]);
                break;
            }
            EditorGUILayout.EndHorizontal();
        }

        //List management. Add new point-types to list or delete list.
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("New Type")) {
            pointsManager.pointTypes.Add(new PointsManager.PointType());
        }
        else if(GUILayout.Button("Delete List")) {
            pointsManager.pointTypes.Clear();
        }
        EditorGUILayout.EndHorizontal();

        //When inspectorvalues have been changed, set object to dirty to allow values to be moved into play mode.
        if (GUI.changed) {
            EditorUtility.SetDirty(pointsManager);
        }
    }
}
