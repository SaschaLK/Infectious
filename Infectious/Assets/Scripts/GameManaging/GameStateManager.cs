using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager instance;

    public bool gameRunning = true;

    private void Awake() {
        instance = this;
    }
}
