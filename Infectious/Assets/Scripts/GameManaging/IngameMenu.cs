using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour {

    public static IngameMenu instance;
    public GameObject quickMenu;

    private void Start() {
        quickMenu.SetActive(false);
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel") && !quickMenu.activeInHierarchy) {
            quickMenu.SetActive(true);
        }
        else if(Input.GetButtonDown("Cancel") && quickMenu.activeInHierarchy) {
            quickMenu.SetActive(false);
        }
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
