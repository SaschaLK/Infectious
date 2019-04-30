using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Glow;

public class Allegiance : MonoBehaviour {

    #region Setup
    //Basic Manager informations
    private float infectionThreshold;
    
    //Manipulatable attributes
    private Material mat;
    private Color baseColor;
    private Color playerPartyColor;

    //States and required Information for calculations
    private float allegianceFactor;
    private int allegianceFactorAccuracy;
    private float baseColorValue;
    private float playerPartyColorValue;
    private bool transitioning;
    public bool hasTransitioned;
    private bool thresholdReached;

    private void Start() {
        FetchInformation();
    }

    private void FetchInformation() {
        //Object components
        mat = GetComponent<Renderer>().material;
        baseColor = mat.color;

        //Manager informations
        playerPartyColor = AllegianceManager.instance.playerPartyColor;
        infectionThreshold = AllegianceManager.instance.infectionThreshold;
        allegianceFactorAccuracy = AllegianceManager.instance.allegianceFactorAccuracy;

        //Local variables and calculation requirements
        baseColorValue = (baseColor.r + baseColor.g + baseColor.b) / 3;
        playerPartyColorValue = (playerPartyColor.r + playerPartyColor.g + playerPartyColor.b) / 3;
    }
    #endregion

    #region Allegiance and ColorChange logic
    private void OnMouseDown() {
        if(AllegianceManager.instance.setLocationCharges > 0) {
            AllegianceManager.instance.setLocationCharges--;
            StartTransition();
        }
    }

    public void StartTransition() {
        if(!transitioning && !hasTransitioned) {
            thresholdReached = false;
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition() {
        transitioning = true;
        while (!hasTransitioned) {
            #region Setup transition information
            //Defines if currently growing or shrinking
            float speed = (PointsManager.instance.unifiedPointsFactor - 1) * Time.deltaTime;

            //If growing or shrinking, set the color accordingly on object
            if (speed > 0) {
                mat.color = new Color(Mathf.Lerp(mat.color.r, playerPartyColor.r, speed), Mathf.Lerp(mat.color.g, playerPartyColor.g, speed), Mathf.Lerp(mat.color.b, playerPartyColor.b, speed));
            }
            else if(speed < 0) {
                mat.color = new Color(Mathf.Lerp(mat.color.r, baseColor.r, speed * - 1), Mathf.Lerp(mat.color.g, baseColor.g, speed * - 1), Mathf.Lerp(mat.color.b, baseColor.b, speed * - 1));
            }

            //Set the current allegianceFactor, floored or ceiled according to if growing or shrinking
            float tempAllegiance = (((mat.color.r + mat.color.g + mat.color.b) / 3) - baseColorValue) / (playerPartyColorValue - baseColorValue);
            if(speed > 0) {
                allegianceFactor = Mathf.Ceil(tempAllegiance * allegianceFactorAccuracy);
            }
            else {
                allegianceFactor = Mathf.Floor(tempAllegiance * allegianceFactorAccuracy);
            }
            #endregion

            #region Logic according to information in Setup transition information
            //Current Logic enables the spreading of infestation before maximum threshold is reached

            //If threshold reached, spread infection
            if (allegianceFactor >= infectionThreshold && !thresholdReached) {
                thresholdReached = true;
                GetComponent<WorldTileBehaviour>().InfestNeighbours();
            }

            //If currently shrinking and no infection, clear neighbours and stop routine
            if(speed < 0 && allegianceFactor <= 0) {
                allegianceFactor = 0;
                GetComponent<WorldTileBehaviour>().ClearNeighbours();
                StopAllCoroutines();
            }

            //If allegianceFactor reached the ceiling set by allegianceFactorAccuracy (10, 100, 1000, ...) -> stop routine
            if(allegianceFactor >= allegianceFactorAccuracy) {
                hasTransitioned = true;
                transitioning = false;
                StopAllCoroutines();
            }
            #endregion

            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
