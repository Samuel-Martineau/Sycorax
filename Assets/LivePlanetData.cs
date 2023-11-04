using UnityEngine;

[System.Serializable]
public class LivePlanetData
{
    public string name;
    public float mass;
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 angularVelocity;

    public static LivePlanetData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<LivePlanetData>(jsonString);
    }
}
