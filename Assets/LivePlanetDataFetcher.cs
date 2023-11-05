using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class LivePlanetDataFetcher : MonoBehaviour
{
    public Texture2D[] planetTextures = new Texture2D[9];

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
    }

    public IEnumerator Load()
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get("https://www.wolframcloud.com/obj/a596e7a7-8d48-46b2-aa18-c2a1fcd091eb");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                (LivePlanetData, Texture2D)[] planets = JsonConvert.DeserializeObject<LivePlanetData[]>(webRequest.downloadHandler.text).Zip(planetTextures, (p, t) => (p, t)).ToArray();
                foreach (var (planet, texture) in planets)
                {
                    GameObject gameObject = gameManager.AddBody(
                        planet.mass,
                        Utilities.arrayToVector3(planet.position),
                        Utilities.arrayToVector3(planet.velocity)
                    );
                    gameObject.GetComponent<Renderer>().material.mainTexture = texture;
                    gameObject.GetComponent<Body>().planetName = planet.name;
                    Destroy(gameObject.GetComponent<RandomTexture>());
                }
                break;
        }
    }
}
