using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelecter : MonoBehaviour {

    private Camera playerCamera;
    private RaycastHit hit;
    private Ray ray;

    private void Start() {
        playerCamera = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(Input.mousePosition);
        }
    }
}
