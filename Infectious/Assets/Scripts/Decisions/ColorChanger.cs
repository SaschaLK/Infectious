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
    public float updateTime = 1.0f;
    private float lerpTimer = 0.0f;
    public float colorFadeTime = 2.0f;
    private Color newColor;
    private Color oldColor;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

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
        
        oldColor = Color.white;
        newColor = Color.white;

        gradient = new Gradient();
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.white;
        colorKey[1].time = 1.0f;
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

    }

    // Update is called once per frame
    void Update() 
    {

        if (timer > updateTime) {


            ProgressUpdate(PointLog.pointLog.Wert011);
            //timer = timer - updateTime;

        }
        

            if (lerpTimer <= colorFadeTime) 
            {
                gradientColor = gradient.Evaluate(lerpTimer / colorFadeTime);
                Debug.Log(gradientColor);
                rend.material.color = gradientColor;
                lerpTimer += Time.deltaTime;
            }
            if (lerpTimer > colorFadeTime) 
            {

                colorKey[0].color = gradientColor;
                gradient.SetKeys(colorKey, alphaKey);
                lerpTimer -= colorFadeTime;
                oldColor = newColor;
                timer = 0.0f;
            }

        
        
        Debug.Log(colorProgress);
        

        timer += Time.deltaTime;




    }

    public void ProgressUpdate(float progressValue) {

        colorProgress = progressValue;
        colorKey[1].color = new Color(1.0f, 1.0f - colorProgress, 1.0f - colorProgress);
        
        oldColor = gradientColor;

        gradient.SetKeys(colorKey, alphaKey);


    }

    public void ResetTimer() {
        timer = 0;
    }
}
    

