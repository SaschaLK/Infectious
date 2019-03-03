using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{

    /*  pollNumbers - Umfragewerte
     *   ecoInfluence - Beliebtheit / Einfluss in der Wirtschaft
     *   mediaInfluence - Beliebtheit / Einfluss in den Medien
     *   ressources - Ressourcen / Geld
     *   reach - Reichweite
     *   
     *   gameProgress - Gesamtfortschritt
    */

    private decimal pollNumbers { get; set; }
    private decimal businessInfluence { get; set; }
    private decimal mediaInfluence { get; set; }
    private decimal ressources { get; set; }
    private int reach { get; set; }
    private float gameProgress { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
