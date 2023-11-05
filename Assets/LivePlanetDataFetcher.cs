using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Linq;

public class LivePlanetDataFetcher : MonoBehaviour
{
    private class LivePlanetData
    {
        public string name;
        public float mass;
        public float[] position;
        public float[] velocity;
    }

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get("https://www.wolframcloud.com/obj/ef34fc8d-12d0-4a44-af3d-45876e4fd470");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                LivePlanetData[] planets = JsonConvert.DeserializeObject<LivePlanetData[]>(webRequest.downloadHandler.text);
                var planet = planets[1];
                gameManager.AddBody(
                    planet.mass,
                    new Vector3(planet.position[0], planet.position[1], planet.position[2]), // TODO: Understand why position scale is wrong
                    new Vector3(planet.velocity[0], planet.velocity[1], planet.velocity[2])
                );
                break;
        }
    }
}
