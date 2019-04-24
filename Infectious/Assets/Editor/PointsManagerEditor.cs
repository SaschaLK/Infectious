using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PointsManager))]
public class PointsManagerEditor : Editor{
    public override void OnInspectorGUI() {
        PointsManager pointsManager = (PointsManager)target;

        //Impact of tendency to points
        pointsManager.tendencyImpactDevider = EditorGUILayout.IntField("Tendency Impact Devider", pointsManager.tendencyImpactDevider);
        EditorGUILayout.Space();

        //List for point-types
        for(int i = 0; i < pointsManager.pointTypes.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            pointsManager.pointTypes[i].name = EditorGUILayout.TextField(pointsManager.pointTypes[i].name);
            if (GUILayout.Button("Delete")) {
                pointsManager.pointTypes.Remove(pointsManager.pointTypes[i]);
                break;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel++;
            pointsManager.pointTypes[i].score = EditorGUILayout.IntField("Current Score", pointsManager.pointTypes[i].score);
            pointsManager.pointTypes[i].nullPoint = EditorGUILayout.FloatField("Null Point", pointsManager.pointTypes[i].nullPoint);
            pointsManager.pointTypes[i].medianPoint = EditorGUILayout.FloatField("Median Point", pointsManager.pointTypes[i].medianPoint);
            pointsManager.pointTypes[i].doublePoint = EditorGUILayout.FloatField("Double Point", pointsManager.pointTypes[i].doublePoint);
            pointsManager.pointTypes[i].tendency = EditorGUILayout.Slider("Tendency", pointsManager.pointTypes[i].tendency, -1, 1);
            EditorGUI.indentLevel--;
            //Maybe implement MinMaxSlider for random ranges?
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
