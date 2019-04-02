using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllegianceManager : MonoBehaviour {

    public static AllegianceManager instance;

    public float transitionTime;
    public Color playerPartyColor;

    private void Awake() {
        instance = this;
    }
}
