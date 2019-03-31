using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Glow;

public class WorldTileBehaviour : MonoBehaviour {

    [Range(0, 10)]
    public float glowStrength;

    private MeshFilter filter;
    private Mesh mesh;
    private List<GameObject> neighbours = new List<GameObject>();
    private Material mat;

    private void Awake() {
        mat = GetComponent<Renderer>().material;

        filter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        filter.mesh = mesh;
    }

    #region SETUP TILE
    public void SetNeighbours(GameObject north, GameObject east, GameObject south, GameObject west) {
        //TO DO: USE NEIGHBOURS TO SPREAD INFECTION
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

        Vector2[] uvs = new Vector2[corners.Length];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(0, 1);

        mesh.Clear();
        mesh.vertices = corners;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        //Set MeshCollider to generated Mesh
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
    #endregion

    private void OnMouseOver() {
        mat.SetFloat("_MKGlowTexStrength", glowStrength);
    }

    private void OnMouseExit() {
        mat.SetFloat("_MKGlowTexStrength", 0f);
    }
}