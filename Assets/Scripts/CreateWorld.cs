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
            createWorld.SetActive(false);
            SelectWorldMenu.SetActive(true);
        }
        else if (webRequestResponse is WebRequestError errorResponse)
        {
            string errorMessage = errorResponse.ErrorMessage;
            Debug.Log("Create environment2D error: " + errorMessage);
            // TODO: Handle error scenario. Show the errormessage to the user.
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
