using System;
using UnityEngine;

static class Utilities
{
    public static float[] vector3ToArray(Vector3 vector)
    {
        return new[] { vector.x, vector.y, vector.z };
    }

    public static Vector3 arrayToVector3(float[] vector)
    {
        return new Vector3(vector[0], vector[1], vector[2]);
    }
}