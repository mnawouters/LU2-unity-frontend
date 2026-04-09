using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class RegisterScript : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject RegisterScene;
    public GameObject RegisterError;
    public GameObject RegisterComplete;
    public TMP_InputField UserNameInput;
    public TMP_InputField PasswordInput;
    public UserApiClient UserAPI;

    public async void createUser()
    {
        string UserName = UserNameInput.text;
        string Password = PasswordInput.text;
        
        IWebRequestReponse webRequestResponse = await UserAPI.Register(new User { Email = UserName, Password = Password });

        if (webRequestResponse is WebRequestData<string> dataResponse)
        {
            Debug.Log("Register succes!");
            RegisterComplete.SetActive(true);
            RegisterError.SetActive(false);
        }
        else if (webRequestResponse is WebRequestError errorResponse)
        {
            string errorMessage = errorResponse.ErrorMessage;
            Debug.Log("Register error: " + errorMessage);
            RegisterError.SetActive(true);
        }
        else
        {
            throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    public void back()
    {
        RegisterScene.SetActive(false);
        StartScreen.SetActive(true);
    }

}

