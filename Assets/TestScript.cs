using UnityEngine;
using UnityEditor;
using System;

public class TestScript : MonoBehaviour
{
    public float mass;
    public Vector3 velocity;
    public Vector3 acceleration;

    public float scale = 5;
    public Gradient gradient;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture(512, 512);
    }

    Texture2D GenerateTexture(int width, int height)
    {
        Texture2D texture = new(width, height);
        OpenSimplexNoise noise = new();

        for (int u = 0; u < width; u++)
        {
            float α = (float)u / (float)width * 2 * Mathf.PI;

            for (int v = 0; v < height; v++)
            {
                float β = (float)v / (float)height * 2 * Mathf.PI;

                float x = Mathf.Cos(α) * Mathf.Cos(β);
                float y = Mathf.Sin(α) * Mathf.Cos(β);
                float z = Mathf.Sin(β);

                float value = (float)noise.Evaluate(
                    x * scale,
                    y * scale,
                    z * scale
                ) / 2 + 0.5f;

                texture.SetPixel(u, v, gradient.Evaluate((float)value));
            }
        }

        texture.Apply();

        return texture;
    }
}

[CustomEditor(typeof(TestScript))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var testScript = target as TestScript;
        if (GUILayout.Button("Initialize"))
        {
            testScript.Initialize();
        }
    }
}
