using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Glow;

public class Allegiance : MonoBehaviour {

    private Material mat;
    private float transitionTime;
    private Color baseColor;
    private Color playerPartyColor;
    private float temp;

    private void Start() {
        //Setup for local information
        mat = GetComponent<Renderer>().material;
        baseColor = mat.color;
        transitionTime = 1 / (AllegianceManager.instance.transitionTime * 10);
        playerPartyColor = AllegianceManager.instance.playerPartyColor;
    }

    private void OnMouseDown() {
        StartCoroutine(Transition());
    }

    public IEnumerator Transition() {
        while((int)(mat.color.r * 100) != (int)(playerPartyColor.r * 100) || (int)(mat.color.g * 100) != (int)(playerPartyColor.g * 100) || (int)(mat.color.b * 100) != (int)(playerPartyColor.b * 100)) {
            mat.color = Color.Lerp(mat.color, playerPartyColor, transitionTime);
            yield return new WaitForFixedUpdate();
        }
        GetComponent<WorldTileBehaviour>().InfestNeighbours();
    }
}
