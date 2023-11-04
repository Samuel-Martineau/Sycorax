using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sphere;
    public float adjustmentFactor;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AddObjects()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log(mousePosition); //make it spawn in front of camera instead
        mousePosition.z += adjustmentFactor;
        Instantiate(sphere, mousePosition, Quaternion.identity);


    }
}
