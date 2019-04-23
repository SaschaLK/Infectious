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
    private List<Situation> situations = new List<Situation>();
    private Situation currentSituation;
    #endregion

    private PointsManager pointsManager;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        //TODO cast list of all situation and min point values -> currentlyAvailableSituations
        pointsManager = GetComponent<PointsManager>();
        LoadSituations();
        DisableAllUIElements();
        StartCoroutine(SituationTimer());
    }

    private void LoadSituations() {
        Object[] temp = Resources.LoadAll<Situation>("");
        foreach(Situation sit in temp) {
            situations.Add(sit);
        }
    }
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
