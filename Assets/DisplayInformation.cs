using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class DisplayInformation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UIDisplay;

   
    void Start()
    {
        UIDisplay = GameObject.FindGameObjectWithTag("PlanetInfo");
    }

    // Update is called once per frame
    void Update()
    {

    
    }
    
    public void DisplayInfo(){
       //UIDisplay.SetActive(!UIDisplay.activeSelf);
        UIDisplay.GetComponent<PlanetInfo>().currentPlanet = gameObject;
    }
}
