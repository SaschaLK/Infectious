using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllegianceManager : MonoBehaviour {

    public static AllegianceManager instance;

    [Range(0.1f, 1f)]
    public float infectionThreshold = 0.8f;
    public Color playerPartyColor;
    public int setLocationCharges = 1;

    private void Awake() {
        instance = this;
    }
}
