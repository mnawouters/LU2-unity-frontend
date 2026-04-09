using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [Header("API Client")]
    public Object2DApiClient object2DApiClient;

    [Header("Screens")]
    public GameObject WorldSelectionScene;
    public GameObject CurrentWorld;
    public GameObject BackButton;
    public Environment2DApiClient environment2DApiClient;

    [Header("Settings")]
    public GameObject[] BeschikbarePrefabs;

    public string CurrentWorldId { get; private set; }

    public float MaxX { get; private set; }
    public float MaxY { get; private set; }

    private List<GameObject> gespawndeObjecten = new List<GameObject>();


    public async void LaadWereldObjecten(string environmentId, Environment2D wereld)
    {
        CurrentWorldId = environmentId;
        MaxX = wereld.MaxLenght;
        MaxY = wereld.MaxHeight;
        CurrentWorld.SetActive(true);

        foreach (GameObject obj in gespawndeObjecten)
            Destroy(obj);
        gespawndeObjecten.Clear();

        IWebRequestReponse response = await object2DApiClient.ReadObject2Ds(environmentId);

        if (response is WebRequestData<List<Object2D>> dataResponse)
        {
            List<Object2D> object2Ds = dataResponse.Data;
            Debug.Log($"Succes: {object2Ds.Count} objecten gevonden.");

            foreach (Object2D objData in object2Ds)
            {
                PlaatsObjectInScene(objData);
            }
        }
        else if (response is WebRequestError errorResponse)
        {
            Debug.LogError("Fout bij ophalen objecten: " + errorResponse.ErrorMessage);
        }
        else
        {
            throw new NotImplementedException("No implementation for response of class: " + response.GetType());
        }
    }

    private void PlaatsObjectInScene(Object2D data)
    {
        if (data.PrefabID < 0 || data.PrefabID >= BeschikbarePrefabs.Length)
        {
            Debug.LogWarning($"PrefabID {data.PrefabID} is niet gevonden in de array.");
            return;
        }

        GameObject prefabOmTeSpawnen = BeschikbarePrefabs[data.PrefabID];

        Vector3 positie = new Vector3((float)data.PositionX, (float)data.PositionY, 0f);
        Quaternion rotatie = Quaternion.Euler(0f, 0f, (float)data.RotationZ);

        GameObject nieuwObject = Instantiate(prefabOmTeSpawnen, positie, rotatie, CurrentWorld.transform);
        gespawndeObjecten.Add(nieuwObject);

        nieuwObject.transform.localScale = new Vector3((float)data.ScaleX, (float)data.ScaleY, 1f);

        SpriteRenderer spriteRenderer = nieuwObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = data.SortingLayer;
        }

        Draggable draggable = nieuwObject.GetComponent<Draggable>();
        if (draggable != null)
        {
            draggable.objectData = data;
            draggable.apiClient = object2DApiClient;
        }
    }

    public void RegistreerObject(GameObject obj)
    {
        gespawndeObjecten.Add(obj);
    }

    public void VerwijderObject(GameObject obj)
    {
        gespawndeObjecten.Remove(obj);
        Destroy(obj);
    }

    public void backToSelectWorld()
    {
        foreach (GameObject obj in gespawndeObjecten)
            Destroy(obj);
        gespawndeObjecten.Clear();

        WorldSelectionScene.SetActive(true);
        CurrentWorld.SetActive(false);
    }
    
        public async void DeleteEnvironment2D()
    {
        IWebRequestReponse webRequestResponse = await environment2DApiClient.DeleteEnvironment(CurrentWorldId);

        if (webRequestResponse is WebRequestData<string> dataResponse)
        {
            Debug.Log("Delete environment2D success");
            backToSelectWorld();
        }
        else if (webRequestResponse is WebRequestError errorResponse)
        {
            string errorMessage = errorResponse.ErrorMessage;
            Debug.Log("Delete environment2D error: " + errorMessage);
        }
        else
        {
            throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }
}
