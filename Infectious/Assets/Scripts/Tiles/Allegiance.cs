using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Glow;

public class Allegiance : MonoBehaviour {

    private Material mat;
    private float transitionTime;
    private Color baseColor;
    private Color playerPartyColor;
    private int playerPartyColorR;
    private int playerPartyColorG;
    private int playerPartyColorB;
    private int multiFactor = 100;
    private bool transitioning;
    private bool hasTransitioned;

    private void Start() {
        //Setup for local information
        mat = GetComponent<Renderer>().material;
        baseColor = mat.color;
        transitionTime = 1 / (AllegianceManager.instance.transitionTime * 10);
        playerPartyColor = AllegianceManager.instance.playerPartyColor;
        playerPartyColorR = Mathf.FloorToInt(playerPartyColor.r * multiFactor);
        playerPartyColorG = Mathf.FloorToInt(playerPartyColor.g * multiFactor);
        playerPartyColorB = Mathf.FloorToInt(playerPartyColor.b * multiFactor);
    }

    private void OnMouseDown() {
        StartTransition();
    }

    public void StartTransition() {
        this.transitioning = true;
        //StartCoroutine(Transition());
    }

    private IEnumerator Transition() {
        Debug.Log("Transition!");
        while((int)(mat.color.r * 100) != (int)(playerPartyColor.r * 100) || (int)(mat.color.g * 100) != (int)(playerPartyColor.g * 100) || (int)(mat.color.b * 100) != (int)(playerPartyColor.b * 100)) {
            mat.color = Color.Lerp(mat.color, playerPartyColor, transitionTime);
            yield return new WaitForFixedUpdate();
        }
        GetComponent<WorldTileBehaviour>().InfestNeighbours();
    }

    private void Update() {
        if (transitioning && !hasTransitioned) {
            mat.color = Color.Lerp(mat.color, playerPartyColor, transitionTime);
            if (Mathf.FloorToInt(mat.color.r * multiFactor) == playerPartyColorR && Mathf.FloorToInt(mat.color.g * multiFactor) == playerPartyColorG && Mathf.FloorToInt(mat.color.b * multiFactor) == playerPartyColorB) {
                transitioning = false;
                hasTransitioned = true;
                GetComponent<WorldTileBehaviour>().InfestNeighbours();
            }
        }
    }
}
