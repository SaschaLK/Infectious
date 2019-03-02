using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public static ColorChanger colorChanger;

    public Color gradientColor = Color.white;
    private float colorProgress;
    Renderer rend;
    public GameObject mapSegment;
    public float compressionRate = 100f;

    private void Awake()
    {
        colorChanger = this;
    }
    
    // Start is called before the first frame update
    void Start() 
    {
        rend = this.GetComponent<Renderer>();
        colorProgress = 0f;
    }

    // Update is called once per frame
    void Update() 
    {
        gradientColor = Color.Lerp(Color.white, Color.red, colorProgress);
        rend.material.color = gradientColor;
        Debug.Log(rend.material.color);
        Debug.Log(colorProgress);

    }

    public void ProgressUpdate(float progressValue) {
        colorProgress = progressValue / compressionRate;
    }
}
    

