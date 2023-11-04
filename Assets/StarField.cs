using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour
{
    public GameObject starPrefab;
    public int starCount = 1_000;
    public float minDistance = 200f;
    public float maxDistance = 500f;
    public float minScale = 1f;
    public float maxScale = 1.5f;

    void Start()
    {
        for (var i = 0; i < 1_000; i++)
        {
            float α = Random.Range(0, 2 * Mathf.PI);
            float β = Random.Range(0, 2 * Mathf.PI);
            float r = Random.Range(minDistance, maxDistance);
            float s = Random.Range(minScale, maxScale);

            float x = r * Mathf.Cos(α) * Mathf.Cos(β);
            float y = r * Mathf.Sin(α) * Mathf.Cos(β);
            float z = r * Mathf.Sin(β);

            GameObject star = Instantiate(starPrefab, new Vector3(x, y, z), Quaternion.identity, this.transform);
            star.transform.localScale = new Vector3(s, s, s);
        }
    }
}
