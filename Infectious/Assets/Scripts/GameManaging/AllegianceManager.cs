using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllegianceManager : MonoBehaviour {

    public static AllegianceManager instance;

    public float transitionTime;
    public Color playerPartyColor;
    public int setLocationCharges = 1;
    public bool gameRunning;

    private void Awake() {
        instance = this;
    }
}
