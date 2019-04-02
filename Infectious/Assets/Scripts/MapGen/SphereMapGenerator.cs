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
    private Dictionary<Vector2Int, GameObject> sphereTiles = new Dictionary<Vector2Int, GameObject>();
    private Vector3 southCap;

    private void Start() {
        longStep = 1/longDensity;
        southCap = new Vector3(0, 0, latDensity / 2 * distanceBetweenElements);

        GenerateSphereTiles();
        PassNeighbors();
        RotateWorld();
    }

    private void GenerateSphereTiles() {
        //The amount of rings
        int Y = 0;
        for (float zPosition = -latDensity / 2; zPosition < latDensity / 2 + 1; zPosition++) {
            float xPosition = 0;
            float yPosition = 0;

            float zCircleChoke = Mathf.Abs(zPosition) != latDensity / 2 ? Mathf.Sqrt(Mathf.Pow((latDensity / 2), 2) - Mathf.Pow(zPosition, 2)) : 1;

            //Each ring
            int X = 0;
            for (float degreeInPi = 0; degreeInPi < 2; degreeInPi += longStep) {
                //Instantiate Tiles
                float x = Mathf.Sin(Mathf.PI * xPosition) * zCircleChoke * distanceBetweenElements;
                float y = Mathf.Cos(Mathf.PI * yPosition) * zCircleChoke * distanceBetweenElements;
                float z = zPosition * distanceBetweenElements;

                if(Mathf.Approximately(zPosition, -latDensity / 2)) {
                    sphereTiles.Add(new Vector2Int(X, Y), Instantiate(tile, new Vector3(x, y, z), Quaternion.identity, gameObject.transform));
                    sphereTiles.Add(new Vector2Int(X, Y - 1), Instantiate(tile, new Vector3(0, 0, z), Quaternion.identity, gameObject.transform));
                }
                else {
                    sphereTiles.Add(new Vector2Int(X, Y), Instantiate(tile, new Vector3(x, y, z), Quaternion.identity, gameObject.transform));
                }
                
                //Move along ring
                xPosition += longStep;
                yPosition += longStep;
                X++;
            }
            Y++;
        }
    }

    private void PassNeighbors() {
        //Setting and passing corner stretch points for each tile
        foreach (KeyValuePair<Vector2Int, GameObject> tile in sphereTiles) {
            //If south most position AND NOT east most position
            if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) && sphereTiles.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetStretchPoints(
                    sphereTiles[new Vector2Int(tile.Key.x + 1, tile.Key.y)].transform.position,
                    southCap,
                    southCap);
            }
            //If south most position AND east most position
            else if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) && !sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetStretchPoints(
                    sphereTiles[new Vector2Int(0, tile.Key.y)].transform.position,
                    southCap,
                    southCap);
            }
            //If NOT south most position BUT east most position
            else if(sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) && !sphereTiles.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetStretchPoints(
                    sphereTiles[new Vector2Int(0, tile.Key.y)].transform.position,
                    sphereTiles[new Vector2Int(0, tile.Key.y + 1)].transform.position,
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y + 1)].transform.position);
            }
            //If regular
            else {
                tile.Value.GetComponent<WorldTileBehaviour>().SetStretchPoints(
                    sphereTiles[new Vector2Int(tile.Key.x + 1, tile.Key.y)].transform.position,
                    sphereTiles[new Vector2Int(tile.Key.x + 1, tile.Key.y + 1)].transform.position,
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y + 1)].transform.position);
            }
        }
        //Setting and passing neighbours for each tile
        foreach(KeyValuePair<Vector2Int, GameObject> tile in sphereTiles) {
            //If top left of grid
            if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) && !sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetNeighbours(
                    null,
                    sphereTiles[new Vector2Int(tile.Key.x + 1, tile.Key.y)],
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y + 1)],
                    sphereTiles[new Vector2Int(Mathf.FloorToInt(latDensity) * 2 - 1, tile.Key.y)]);
            }
            //If top middle of grid
            else if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) && sphereTiles.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) && sphereTiles.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetNeighbours(
                    null,
                    sphereTiles[new Vector2Int(tile.Key.x + 1, tile.Key.y)],
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y + 1)],
                    sphereTiles[new Vector2Int(tile.Key.x - 1, tile.Key.y)]);
            }
            //If top right of grid
            else if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1)) && !sphereTiles.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetNeighbours(
                    null,
                    sphereTiles[new Vector2Int(0, tile.Key.y)],
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y + 1)],
                    sphereTiles[new Vector2Int(tile.Key.x - 1, tile.Key.y)]);
            }
            //If left middle of grid
            else if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) && sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) && sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetNeighbours(
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y - 1)],
                    sphereTiles[new Vector2Int(tile.Key.x + 1, tile.Key.y)],
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y + 1)],
                    sphereTiles[new Vector2Int(Mathf.FloorToInt(latDensity) * 2 - 1, tile.Key.y)]);

            }
            //If right middle of grid
            else if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) && sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) && sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y - 1))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetNeighbours(
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y - 1)],
                    sphereTiles[new Vector2Int(0, tile.Key.y)],
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y + 1)],
                    sphereTiles[new Vector2Int(tile.Key.x - 1, tile.Key.y)]);
            }
            //If bottom left of grid
            else if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) && !sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetNeighbours(
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y - 1)],
                    sphereTiles[new Vector2Int(tile.Key.x + 1, tile.Key.y)],
                    null,
                    sphereTiles[new Vector2Int(Mathf.FloorToInt(latDensity) * 2 - 1, tile.Key.y)]);
            }
            //If bottom middle of grid
            else if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1)) && sphereTiles.ContainsKey(new Vector2Int(tile.Key.x - 1, tile.Key.y)) && sphereTiles.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetNeighbours(
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y - 1)],
                    sphereTiles[new Vector2Int(tile.Key.x + 1, tile.Key.y)],
                    null,
                    sphereTiles[new Vector2Int(tile.Key.x - 1, tile.Key.y)]);
            }
            //If bottom right of grid
            else if(!sphereTiles.ContainsKey(new Vector2Int(tile.Key.x + 1, tile.Key.y)) && !sphereTiles.ContainsKey(new Vector2Int(tile.Key.x, tile.Key.y + 1))) {
                tile.Value.GetComponent<WorldTileBehaviour>().SetNeighbours(
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y - 1)],
                    sphereTiles[new Vector2Int(0, tile.Key.y)],
                    null,
                    sphereTiles[new Vector2Int(tile.Key.x - 1, tile.Key.y)]);
            }
            //If middle of grid
            else {
                tile.Value.GetComponent<WorldTileBehaviour>().SetNeighbours(
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y - 1)],
                    sphereTiles[new Vector2Int(tile.Key.x + 1, tile.Key.y)],
                    sphereTiles[new Vector2Int(tile.Key.x, tile.Key.y + 1)],
                    sphereTiles[new Vector2Int(tile.Key.x - 1, tile.Key.y)]);
            }
        }
    }

    private void RotateWorld() {
        gameObject.transform.Rotate(new Vector3(90, 0, 0));
    }
}
