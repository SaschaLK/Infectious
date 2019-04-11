using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationManager : MonoBehaviour {

    public static SituationManager instance;

    public List<Situation> situations = new List<Situation>();

    private void Awake() {
        instance = this;
    }
}
