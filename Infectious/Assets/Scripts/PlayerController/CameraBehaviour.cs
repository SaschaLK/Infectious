using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject world;
    public float distance;
    private Vector3 position = Vector3.up;

    private void Start() {
        float sphereBuffer = world.GetComponent<SphereMapGenerator>().distanceBetweenElements;
        position = position * sphereBuffer * distance;
        transform.position = position;
    }

    private void Update() {
        transform.LookAt(world.transform);
    }
}
