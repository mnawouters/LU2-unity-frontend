using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateWorld : MonoBehaviour
{
    public TMP_InputField worldNameInputField;
    public TMP_InputField WorldLenghtInput;
    public TMP_InputField WorldHeightInput;
    public Environment2DApiClient EnviromentAPI;
    public GameObject createWorld;
    public GameObject SelectWorldMenu;
    public TMP_Text ErrorText;

    public async void CreateEnvironment2D()
    {
        Environment2D NieuweEnviroment = new Environment2D()
        {
            //Input text van de input fields voor het creeeren van een nieuwe wereld
            Name = worldNameInputField.text,
            MaxHeight = Convert.ToInt32(WorldHeightInput.text),
            MaxLenght = Convert.ToInt32(WorldLenghtInput.text),
        };
        //API word aangeroepen om een nieuwe wereld te maken
        IWebRequestReponse webRequestResponse = await EnviromentAPI.CreateEnvironment(NieuweEnviroment);

        if (webRequestResponse is WebRequestData<Environment2D> dataResponse)
        {
            NieuweEnviroment.Id = dataResponse.Data.Id;
            if (ErrorText != null) ErrorText.gameObject.SetActive(false);
            createWorld.SetActive(false);
            SelectWorldMenu.SetActive(true);
        }
        else if (webRequestResponse is WebRequestError errorResponse)
        {
            Debug.Log("Create environment2D error: " + errorResponse.ErrorMessage);
            if (ErrorText != null)
            {
                ErrorText.text = "Fout: naam, hoogte (10-100) en breedte (10-200) zijn verplicht.";
                ErrorText.gameObject.SetActive(true);
            }
        }
        else
        {
            throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    public void BackToSelect()
    {
        createWorld.SetActive(false);
        SelectWorldMenu.SetActive(true);

    }
}
