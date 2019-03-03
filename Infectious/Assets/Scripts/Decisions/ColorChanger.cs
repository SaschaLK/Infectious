using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public static ColorChanger colorChanger;

    private float colorProgress;
    private Renderer rend;
    private float timer;
    private float fadeTimer = 0.0f;
    private Color newColor;
    private Color oldColor;
    private Gradient gradient;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;


    public GameObject mapSegment;

    public Color gradientColor = Color.white;
    public float updateTime = 1.0f;
    public float colorFadeLength = 2.0f;

    
    
  

    private void Awake()
    {
        colorChanger = this;

    }
    
    
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

    
    void Update() 
    {

        //Die Klasse ruft in regelmäßigen Abständen den aktuellen Wert von der PointLog Klasse ab und gibt sie an die ProgressUpdate Methode ab.
        if (timer > updateTime) {

            ProgressUpdate(PointLog.pointLog.Wert011);
            //timer = timer - updateTime;

        }
        
            //Das Object wird langsam eingefärbt
            if (fadeTimer <= colorFadeLength) 
            {
                gradientColor = gradient.Evaluate(fadeTimer / colorFadeLength);
                Debug.Log(gradientColor);
                rend.material.color = gradientColor;
                fadeTimer += Time.deltaTime;
            }
            if (fadeTimer > colorFadeLength) 
            {

                colorKey[0].color = gradientColor;
                gradient.SetKeys(colorKey, alphaKey);
                fadeTimer -= colorFadeLength;
                oldColor = newColor;
                timer -=colorFadeLength;
            }

        Debug.Log(colorProgress);
        
        timer += Time.deltaTime;
        
    }

    //Die Methode stellt anhand des aktuellen Wertes die neue Farbe ein, in der da Objekt eingefärbt wird.
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
    

