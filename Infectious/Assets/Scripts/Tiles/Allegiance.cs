﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Glow;

public class Allegiance : MonoBehaviour {

    #region Setup
    //Basic Manager informations
    private float transitionTime;
    
    //Manipulatable attributes
    private Material mat;
    private Color baseColor;
    private Color playerPartyColor;
    private int playerPartyColorR;
    private int playerPartyColorG;
    private int playerPartyColorB;
    private int multiFactor = 100;

    //States and required Information for calculations
    private bool transitioning;
    private bool hasTransitioned;

    private void Start() {
        FetchManagerInformation();
    }

    private void FetchManagerInformation() {
        mat = GetComponent<Renderer>().material;
        baseColor = mat.color;
        transitionTime = 1 / (AllegianceManager.instance.transitionTime * 10);
        playerPartyColor = AllegianceManager.instance.playerPartyColor;
        playerPartyColorR = Mathf.FloorToInt(playerPartyColor.r * multiFactor);
        playerPartyColorG = Mathf.FloorToInt(playerPartyColor.g * multiFactor);
        playerPartyColorB = Mathf.FloorToInt(playerPartyColor.b * multiFactor);
    }
    #endregion

    #region Allegiance and ColorChange logic
    private void OnMouseDown() {
        if(AllegianceManager.instance.setLocationCharges > 0) {
            StartTransition();
        }
    }

    public void StartTransition() {
        //Option to enable multiple coroutine and therefore set a faster transitioning rate.
        if(!transitioning && !hasTransitioned) {
            AllegianceManager.instance.setLocationCharges--;
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition() {
        transitioning = true;
        while (!hasTransitioned) {
            mat.color = Color.Lerp(mat.color, playerPartyColor, transitionTime);
            if (Mathf.FloorToInt(mat.color.r * multiFactor) == playerPartyColorR && Mathf.FloorToInt(mat.color.g * multiFactor) == playerPartyColorG && Mathf.FloorToInt(mat.color.b * multiFactor) == playerPartyColorB) {
                hasTransitioned = true;
                transitioning = false;
                GetComponent<WorldTileBehaviour>().InfestNeighbours();
                StopAllCoroutines();
            }
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
