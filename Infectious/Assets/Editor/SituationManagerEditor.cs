using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SituationManager))]
public class SituationManagerEditor : Editor {
    //public override void OnInspectorGUI() {
    //    SituationManager situationManager = (SituationManager)target;

    //    EditorGUILayout.LabelField("Situation Event Settings");
    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.LabelField("Time Delay");
    //    situationManager.timeDelay = EditorGUILayout.FloatField(situationManager.timeDelay);
    //    EditorGUILayout.EndHorizontal();
    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.LabelField("Minimum Time Delay");
    //    situationManager.minTimeDelay = EditorGUILayout.FloatField(situationManager.minTimeDelay);
    //    EditorGUILayout.EndHorizontal();
    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.LabelField("Maximum Time Delay");
    //    situationManager.maxTimeDelay = EditorGUILayout.FloatField(situationManager.maxTimeDelay);
    //    EditorGUILayout.EndHorizontal();

    //    EditorGUILayout.LabelField("Situation List");
    //    //situationManager.situationsPath = EditorUtility.OpenFolderPanel("Select Folder", "", "");
    //    //situationManager.situationsPath = EditorGUILayout.TextField(EditorUtility.OpenFolderPanel("Select Folder", "", ""));
    //}
}
