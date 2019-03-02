using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLog : MonoBehaviour
{
    //public object segment;
    //public ColorChanger segment;
    private float Wert01 = 0.0f;
    private float tickrateCURRENT = 1.0f;
    private float tickrateNEW = 1.1f;
    public float baseTick = 1.1f;
    public float tick = 5.0f;
    private float timer = 0.0f;

    GameObject LM;
         
    
    // Start is called before the first frame update
    void Start()
    {
        //ColorChanger colorChanger = LM.GetComponent<ColorChanger>();
        LM = this.gameObject;
        Wert01 += 1.0f;
        tickrateNEW = baseTick;
        //ColorChanger colorChanger = LM.AddComponent<ColorChanger>;
        //segment = new ColorChanger();   
        //var test = this.gameObject.GetComponent<ColorChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        ColorChanger.colorChanger.ProgressUpdate(Wert01);
        //LM.GetComponent<ColorChanger>().ProgressUpdate(Wert01);

        timer += Time.deltaTime;
        if (timer > tick) 
        {

            Wert01 = Wert01 + tickrateNEW;
            tickrateCURRENT = tickrateNEW;
            timer = timer - tick;
            Debug.Log(Wert01);
            Debug.Log(tickrateNEW);
            Debug.Log(baseTick);
        }

       
    }

    public void AddValue() 
    {
        tickrateNEW = tickrateCURRENT * 1.1f;
    }

    public void SubtractValue() 
    {
        tickrateNEW = tickrateCURRENT * 0.9f;
    }
}
