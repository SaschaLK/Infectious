using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBehaviour : MonoBehaviour {

    public GameObject world;
    public float surfaceDistance;
    [Range(0.1f, 5)]
    public float cameraSpeed = 1.5f;
    [Range(0.1f, 10)]
    public float scrollSensitivity = 1;
    private float distance;
    private Camera cam;

    private void Awake() {
        if (world == null) {
            Debug.Log("Camera requires world object to be linked");
        }
    }

    private void Start() {
        //Set Start position of camera in proportion to worldsphere
        float worldSize = world.GetComponent<SphereMapGenerator>().latDensity / 2 * world.GetComponent<SphereMapGenerator>().distanceBetweenElements;
        cam = GetComponent<Camera>();
        distance = worldSize + surfaceDistance;
        transform.position = new Vector3(distance, 0, 0);
    }

    private void Update() {
        //Movement Vectors (disregarding additional Speed if multiple vectors are active
        //TODO: Fix max speed of camera
        if (Input.GetButton("Horizontal")) {
            Quaternion camTurnAngleH = Quaternion.AngleAxis(Input.GetAxis("Horizontal") * -cameraSpeed, Vector3.up);
            transform.position = camTurnAngleH * transform.position;
        }
        if (Input.GetButton("Vertical")) {
            //TODO: fix camera vertical rotation to always go over poles
            //Notes:            
                    //Rotation around dynamic axis:
                    //The higher the z value, the more rotation around the x axes
                    //The higher the x value, the more rotation around the z axes
                    //If z value = 0, rotate around x axes
                    //What about the poles? 
            Quaternion camTurnAngleV = Quaternion.AngleAxis(Input.GetAxis("Vertical") * cameraSpeed, Vector3.forward);
            transform.position = camTurnAngleV * transform.position;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            //TODO: Maybe set min and max cameraSize?
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        }

        //Setting view direction
        transform.LookAt(world.transform);
    }

    public void ChangeCameraSpeed(Slider slider) {
        cameraSpeed = slider.value;
    }

    public void ChangeScrollSensitivity(Slider slider) {
        scrollSensitivity = slider.value;
    }
}
