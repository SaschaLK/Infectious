using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SituationManager : MonoBehaviour {

    public static SituationManager instance;

    public float timeDelay;
    public float minTimeDelay;
    public float maxTimeDelay;
    // = EditorUtility.OpenFolderPanel("Open Folder", "", "")
    public string situationsPath;
    public List<Situation> situations = new List<Situation>();

    private PointsManager pointsManager;

    private void Awake() {
        instance = this;

        pointsManager = GetComponent<PointsManager>();
    }

    private void Start() {
        LoadAllSituationsFromFolder();

        //TODO cast list of all situation and min point values
    }

    private void Update() {
        timeDelay -= Time.deltaTime;
        if(timeDelay <= 0) {
            timeDelay = Random.Range(minTimeDelay, maxTimeDelay);
            //Debug.Log("Next in: " + timeDelay);
            //Debug.Log(Time.time);
            SituationEvent();
        }
    }

    private void LoadAllSituationsFromFolder() {
        //TODO: Loading Situation from Folder and selecting folder in Editor




        //Object[] temp = Resources.LoadAll(situationsPath);
        //foreach(Object o in temp) {
        //    Debug.Log("tem");
        //}
        //foreach(Resources sit in Resources.LoadAll<ScriptableObject>(situationsPath)) {
        //    Debug.Log("hello");
        //}
    }

    private void SituationEvent() {
        //Time.timeScale = 0;
        Situation temp = SelectSituation();
    }

    private Situation SelectSituation() {
        List<Situation> availableSituations = new List<Situation>();

        //Foreach situation check every pointType and compare to current pointType Value. If threshold reached, add to availableSituations
        for(int i = 0; i < situations.Count; i++) {
            bool isAvailable = true;
            for (int k = 0; k < pointsManager.pointTypes.Count; k++) {
                if (pointsManager.pointTypes[k].value < situations[i].minPointValues[k]) {
                    isAvailable = false;
                }
            }
            if (isAvailable) {
                availableSituations.Add(situations[i]);
            }
        }
        Debug.Log(availableSituations.Count);

        if(availableSituations.Count == 0) {
            return null;
        }
        else {
            return availableSituations[Random.Range(0, availableSituations.Count)];
        }
    }
}
