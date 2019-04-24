﻿using System.Collections;
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
    private float baseColorValue;
    private float playerPartyColorValue;
    private bool transitioning;
    private bool hasTransitioned;

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
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition() {
        transitioning = true;
        while (!hasTransitioned) {
            float speed = (PointsManager.instance.unifiedPointsFactor - 1) * Time.deltaTime;
            if(speed != 0) {
                mat.color = new Color(Mathf.Lerp(mat.color.r, playerPartyColor.r, speed), Mathf.Lerp(mat.color.g, playerPartyColor.g, speed), Mathf.Lerp(mat.color.b, playerPartyColor.b, speed));
            }
            allegianceFactor = (mat.color.r + mat.color.g + mat.color.b) / 3;
            allegianceFactor = (allegianceFactor - baseColorValue) / (playerPartyColorValue - baseColorValue);
            if(allegianceFactor > infectionThreshold) {
                GetComponent<WorldTileBehaviour>().InfestNeighbours();
            }
            if(allegianceFactor >= 1) {
                hasTransitioned = true;
                transitioning = false;
                StopAllCoroutines();
            }
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
