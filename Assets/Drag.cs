
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
/*
    private Vector3 mOffset;
    private float mZCoord;

    public void drag()
    {
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        transform.position = GetMouseWorldPos() + mOffset;
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }

    private Vector3 GetMouseWorldPos()
    {
        // pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

*/


    Vector3 mousePosition;

    private Vector3 GetMousePosition (){
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown() {
        mousePosition = Input.mousePosition - GetMousePosition();
    }


    private void OnMouseDrag(){
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
}

