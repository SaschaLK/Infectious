using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllegianceManager : MonoBehaviour {

    public static AllegianceManager instance;

    [Range(1, 100)]
    public float infectionThreshold = 0.8f;
    public Color playerPartyColor;
    public int setLocationCharges = 1;
    public int allegianceFactorAccuracy = 100;

    private void Awake() {
        instance = this;
    }
}
