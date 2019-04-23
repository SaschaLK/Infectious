using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SituationManager : MonoBehaviour {
    #region Setup
    public static SituationManager instance;

    #region Situation canvas
    public GameObject situationCanvas;
    public Text situationLabel;
    public Text situationDescription;
    public List<GameObject> buttons;
    #endregion

    #region Situations and Times
    public float timeDelay;
    public float minTimeDelay;
    public float maxTimeDelay;
    public string situationsPath;
    public List<Situation> situations = new List<Situation>();
    private Situation currentSituation;
    // = EditorUtility.OpenFolderPanel("Open Folder", "", "")
    #endregion

    private PointsManager pointsManager;

    private void Awake() {
        instance = this;

        pointsManager = GetComponent<PointsManager>();
    }

    private void Start() {
        //LoadAllSituationsFromFolder();
        //TODO cast list of all situation and min point values
        DisableAllUIElements();
        StartCoroutine(SituationTimer());
    }
    #endregion
    #region Legacy and trying to load Situation from folder at start.
    //private void Update() {
    //    timeDelay -= Time.deltaTime;
    //    if(timeDelay <= 0) {
    //        timeDelay = Random.Range(minTimeDelay, maxTimeDelay);
    //        //Debug.Log("Next in: " + timeDelay);
    //        //Debug.Log(Time.time);
    //        SituationEvent();
    //    }
    //}

    //private void LoadAllSituationsFromFolder() {
    //    //TODO: Loading Situation from Folder and selecting folder in Editor

    //    //Object[] temp = Resources.LoadAll(situationsPath);
    //    //foreach(Object o in temp) {
    //    //    Debug.Log("tem");
    //    //}
    //    //foreach(Resources sit in Resources.LoadAll<ScriptableObject>(situationsPath)) {
    //    //    Debug.Log("hello");
    //    //}
    //}

    //private void SituationEvent() {
    //    //Time.timeScale = 0;
    //    Situation temp = SelectSituation();
    //}

    //private Situation SelectSituation() {
    //    List<Situation> availableSituations = new List<Situation>();

    //    //Foreach situation check every pointType and compare to current pointType Value. If threshold reached, add to availableSituations
    //    for(int i = 0; i < situations.Count; i++) {
    //        bool isAvailable = true;
    //        for (int k = 0; k < pointsManager.pointTypes.Count; k++) {
    //            if (pointsManager.pointTypes[k].score < situations[i].minPointValues[k]) {
    //                isAvailable = false;
    //            }
    //        }
    //        if (isAvailable) {
    //            availableSituations.Add(situations[i]);
    //        }
    //    }

    //    if(availableSituations.Count == 0) {
    //        return null;
    //    }
    //    else {
    //        return availableSituations[Random.Range(0, availableSituations.Count)];
    //    }
    //}
    #endregion

    private IEnumerator SituationTimer() {
        timeDelay = Random.Range(minTimeDelay, maxTimeDelay);
        yield return new WaitForSecondsRealtime(timeDelay);
        CastSituation();
    }

    private void CastSituation() {
        pointsManager.StopAllCoroutines();
        situationCanvas.SetActive(true);
        currentSituation = situations[Random.Range(0, situations.Count)];
        situationLabel.text = currentSituation.name;
        situationDescription.text = currentSituation.situationText;

        for(int i = 0; i < currentSituation.decisions.Count; i++) {
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<Text>().text = currentSituation.decisions[i].decisionName;
        }
    }

    public void ActivateDecision(int dec) {
        pointsManager.UpdatePoints(currentSituation.decisions[dec]);
        DisableAllUIElements();
        StartCoroutine(SituationTimer());
    }

    private void DisableAllUIElements() {
        foreach (GameObject button in buttons) {
            button.SetActive(false);
        }
        situationCanvas.SetActive(false);
    }
}
