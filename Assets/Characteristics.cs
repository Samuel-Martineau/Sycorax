using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Characteristics : MonoBehaviour
{
    // Start is called before the first frame update
    public PlanetList PList;


    public float mass = 10;
    public string planetName="Default";
    
    

    void Start()
    {
        PList = GameObject.FindGameObjectWithTag("PublicFunctions").GetComponent<PlanetList>();

        int i = Random.Range(0, PList.nameList.Count);
        planetName = PList.nameList[i];
        PList.nameList.Remove(PList.nameList[i]);

    }

    // 

    // string generateName(){
    //     string name = "";
    //     int length = Random.Range(3,10);
    //     for(int i =0; i<length; i++){
    //         int l = Random.Range(0,26);
    //         char randChar = (char)('a'+l);
    //         name = name.Insert(i,randChar);
    //     }
    //     return name;
    // }
    //Update is called once per frame
    void Update()
    {
        
    }


}
