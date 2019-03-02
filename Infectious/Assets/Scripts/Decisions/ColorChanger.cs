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
    private float timer;
    public float transitionlength;
    private Color newColor;
    private Color oldColor;

    private void Awake()
    {
        colorChanger = this;
    }
    
    // Start is called before the first frame update
    void Start() 
    {
        rend = this.GetComponent<Renderer>();
        colorProgress = 0f;
        timer = 0.0f;
        transitionlength = 2.0f;
        oldColor = Color.white;
        newColor = Color.white;
    }

    // Update is called once per frame
    void Update() 
    {


        gradientColor = Color.Lerp(oldColor, newColor, (1 * (timer / transitionlength)));
        rend.material.color = gradientColor;
        //newColor = new Color(1.0f, 1.0f - colorProgress, 1.0f - colorProgress);
       // Debug.Log(rend.material.color);
        Debug.Log(colorProgress);
        //Debug.Log(newColor);

        timer += Time.deltaTime;
        //if (timer > transitionlength) {
            
        //    timer = timer - transitionlength;
            
        //}



    }

    public void ProgressUpdate(float progressValue) {

        colorProgress = progressValue;
        newColor = Color.Lerp(Color.white, Color.red, colorProgress);
        oldColor = gradientColor;
        
    }

    public void ResetTimer() {
        timer = 0;
    }
}
    

