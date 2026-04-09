using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class WorldSelectionScript : MonoBehaviour
{
    public GameObject World;
    public GameObject SelectWorldScherm;
    public WorldManager WorldManager;
    public GameObject CreateWorld;


    public Environment2DApiClient environment2DApiClient;

    [Header("Wereld buttons")]
    public GameObject Wereld1Button;
    public GameObject Wereld2Button;
    public GameObject Wereld3Button;
    public GameObject Wereld4Button;
    public GameObject Wereld5Button;
    public GameObject CreateWorldButton;

    public async void OnEnable()
    {
        GameObject[] wereldKnoppen = new GameObject[] { Wereld1Button, Wereld2Button, Wereld3Button, Wereld4Button, Wereld5Button };

        IWebRequestReponse webRequestResponse = await environment2DApiClient.ReadEnvironment2Ds();

        if (webRequestResponse is WebRequestData<List<Environment2D>> dataResponse)
        {
            List<Environment2D> environment2Ds = dataResponse.Data;
            Debug.Log($"Aantal werelden opgehaald: {environment2Ds.Count}");

            
            for (int i = 0; i < wereldKnoppen.Length; i++)
            {
                GameObject Knop = wereldKnoppen[i];

                if (i < environment2Ds.Count)
                {
                    Environment2D geselecteerdeWereld = environment2Ds[i];

                    Knop.SetActive(true);

                    TMP_Text knopTekst = Knop.GetComponentInChildren<TMP_Text>();
                    if (knopTekst != null)
                    {
                        knopTekst.text = geselecteerdeWereld.Name; 
                    }

                    Button buttonComponent = Knop.GetComponent<Button>();
                    if (buttonComponent != null)
                    {
                        buttonComponent.onClick.RemoveAllListeners();

                        
                        Environment2D wereldRef = geselecteerdeWereld;

                        buttonComponent.onClick.AddListener(() => LaadWereld(wereldRef));
                    }
                }
                else
                {
                    Knop.SetActive(false);
                }
            }
        }
        else if (webRequestResponse is WebRequestError errorResponse)
        {
            string errorMessage = errorResponse.ErrorMessage;
            Debug.Log("Read environment2Ds error: " + errorMessage);
        }
        else
        {
            throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    private void LaadWereld(Environment2D wereld)
    {
        Debug.Log($"Wereld geselecteerd: {wereld.Name} met ID: {wereld.Id}");

        SelectWorldScherm.SetActive(false);
        World.SetActive(true);

        if (WorldManager.Instance != null)
        {
            WorldManager.Instance.LaadWereldObjecten(wereld.Id, wereld);
        }
    }
    public void ToCreateWorld()
    {
        CreateWorld.SetActive(true);
        SelectWorldScherm.SetActive(false);
    }
   
}
