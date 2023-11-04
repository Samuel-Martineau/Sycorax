using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sphere;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddObjects(){
        Instantiate(sphere, new Vector3( 0, 0, 0), Quaternion.identity);
        
    }
}
