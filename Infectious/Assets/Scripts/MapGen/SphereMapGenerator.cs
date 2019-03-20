using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMapGenerator : MonoBehaviour {

    public static SphereMapGenerator instance;

    private void Awake() {
        instance = this;
    }

    public GameObject tile;
    public float distanceBetweenElements;
    public float latDensity;
    public float longDensity;
    private float longStep;

    private Dictionary<Vector2, GameObject> sphereTiles = new Dictionary<Vector2, GameObject>();

    private Vector3 southCap;

    private void Start() {
        longStep = 1/longDensity;
        southCap = new Vector3(0, 0, latDensity / 2 * distanceBetweenElements);

        GenerateSphereTiles();
        PassNeighbors();
    }

    private void GenerateSphereTiles() {
        //The amount of rings
        for (float zPosition = -latDensity / 2; zPosition < latDensity / 2 + 1; zPosition++) {
            float xPosition = 0;
            float yPosition = 0;

            float zCircleChoke = Mathf.Abs(zPosition) != latDensity / 2 ? Mathf.Sqrt(Mathf.Pow((latDensity / 2), 2) - Mathf.Pow(zPosition, 2)) : 1;

            //Each ring
            for (float degreeInPi = 0; degreeInPi < 2; degreeInPi += longStep) {
                //Instantiate Tiles
                float x = Mathf.Sin(Mathf.PI * xPosition) * zCircleChoke * distanceBetweenElements;
                float y = Mathf.Cos(Mathf.PI * yPosition) * zCircleChoke * distanceBetweenElements;
                float z = zPosition * distanceBetweenElements;

                if(Mathf.Approximately(zPosition, -latDensity / 2)) {
                    sphereTiles.Add(new Vector2(degreeInPi, zPosition), Instantiate(tile, new Vector3(x, y, z), Quaternion.identity, gameObject.transform));
                    sphereTiles.Add(new Vector2(degreeInPi, zPosition - 1), Instantiate(tile, new Vector3(0, 0, z), Quaternion.identity, gameObject.transform));
                }
                else {
                    sphereTiles.Add(new Vector2(degreeInPi, zPosition), Instantiate(tile, new Vector3(x, y, z), Quaternion.identity, gameObject.transform));
                }
                
                //Move along ring
                xPosition += longStep;
                yPosition += longStep;
            }
        }
    }

    private void PassNeighbors() {
        foreach (KeyValuePair<Vector2, GameObject> tile in sphereTiles) {
            //If south most position AND NOT east most position
            if (Mathf.Approximately(tile.Key.y, latDensity / 2) && !Mathf.Approximately(tile.Key.x, 2f - longStep)) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetStretchPoints(
                    sphereTiles[new Vector2(tile.Key.x + longStep, tile.Key.y)].transform.position,
                    southCap,
                    southCap);
            }
            //If south most position AND east most position
            else if ((Mathf.Approximately(tile.Key.y, latDensity / 2) && Mathf.Approximately(tile.Key.x, 2f - longStep))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetStretchPoints(
                    sphereTiles[new Vector2(0, tile.Key.y)].transform.position,
                    southCap,
                    southCap);
            }
            //If NOT south most position BUT east most position
            else if (!Mathf.Approximately(tile.Key.y, latDensity / 2 ) && Mathf.Approximately(tile.Key.x, 2f - longStep)) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetStretchPoints(
                    sphereTiles[new Vector2(0, tile.Key.y)].transform.position,
                    sphereTiles[new Vector2(0, tile.Key.y + 1)].transform.position,
                    sphereTiles[new Vector2(tile.Key.x, tile.Key.y + 1)].transform.position);
            }
            //If regular
            else {
                tile.Value.GetComponent<WorldTileBehaviour>().SetStretchPoints(
                    sphereTiles[new Vector2(tile.Key.x + longStep, tile.Key.y)].transform.position,
                    sphereTiles[new Vector2(tile.Key.x + longStep, tile.Key.y + 1)].transform.position,
                    sphereTiles[new Vector2(tile.Key.x, tile.Key.y + 1)].transform.position);
            }
        }
    }
}
