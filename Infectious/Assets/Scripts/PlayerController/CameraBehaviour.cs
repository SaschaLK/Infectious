using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject world;
    public float surfaceDistance;
    public float cameraSpeed;
    private float distance;

    private void Awake() {
        if (world == null) {
            Debug.Log("Camera requires world object to be linked");
        }
    }

    private void Start() {
        //Set Starting position
        float worldSize = world.GetComponent<SphereMapGenerator>().latDensity / 2 * world.GetComponent<SphereMapGenerator>().distanceBetweenElements;
        distance = worldSize + surfaceDistance;
        transform.position = new Vector3(distance, 0, 0);
    }

    private void Update() {

        if (Input.GetButton("Horizontal")) {
            Quaternion camTurnAngleH = Quaternion.AngleAxis(Input.GetAxis("Horizontal") * -cameraSpeed, Vector3.up);
            transform.position = camTurnAngleH * transform.position;
        }
        if (Input.GetButton("Vertical")) {
            Quaternion camTurnAngleV = Quaternion.AngleAxis(Input.GetAxis("Vertical") * cameraSpeed, Vector3.forward);
            transform.position = camTurnAngleV * transform.position;
        }
        transform.LookAt(world.transform);
    }
}
