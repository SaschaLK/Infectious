using System.Collections;
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
            //mat.color = Color.Lerp(mat.color, playerPartyColor, transitionTime);
            //if (Mathf.FloorToInt(mat.color.r * multiFactor) == playerPartyColorR && Mathf.FloorToInt(mat.color.g * multiFactor) == playerPartyColorG && Mathf.FloorToInt(mat.color.b * multiFactor) == playerPartyColorB) {
            //    hasTransitioned = true;
            //    transitioning = false;
            //    GetComponent<WorldTileBehaviour>().InfestNeighbours();
            //    StopAllCoroutines();
            //}
            float temp = PointsManager.instance.unifiedPointsFactor - 1;
            if(temp > 0) {
                mat.color = new Color(Mathf.Lerp(baseColor.r, playerPartyColor.r, temp), Mathf.Lerp(baseColor.g, playerPartyColor.g, temp), Mathf.Lerp(baseColor.b, playerPartyColor.b, temp));
            }
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
