using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileBehaviour : MonoBehaviour {

    private MeshFilter filter;
    private Mesh mesh;

    private List<GameObject> neighbours = new List<GameObject>();

    private void Awake() {
        filter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        filter.mesh = mesh;
    }

    public void SetNeighbours(GameObject north, GameObject east, GameObject south, GameObject west) {
        neighbours.Add(north);
        neighbours.Add(east);
        neighbours.Add(south);
        neighbours.Add(west);
    }

    public void SetStretchPoints(Vector3 b, Vector3 c, Vector3 d) {
        Vector3 localA = transform.InverseTransformPoint(transform.position);
        Vector3 localB = transform.InverseTransformPoint(b);
        Vector3 localC = transform.InverseTransformPoint(c);
        Vector3 localD = transform.InverseTransformPoint(d);

        Vector3[] corners = new Vector3[4];
        corners[0] = localA;
        corners[1] = localB;
        corners[2] = localC;
        corners[3] = localD;

        Build(corners);
    }

    private void Build(Vector3[] corners) {
        int[] triangles = new int[] {
            2, 1, 0,
            2, 0, 3
        };

        mesh.Clear();
        mesh.vertices = corners;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}