using JetBrains.Annotations;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LoginScript : MonoBehaviour
{
    public GameObject InlogScene;
    public GameObject RegisterScene;
    public GameObject WereldSelect;
    public GameObject StartScreen;
    public TMP_InputField UsernameInput;
    public TMP_InputField WachtwoordInput;
    private string UserEmail;
    private string UserWachtwoord;
    public UserApiClient UserAPI;
    public TMP_Text InlogErrorTekst;

    public void Register()
    {
        InlogScene.SetActive(false);
        RegisterScene.SetActive(true);
    }

    public void BackToStartScreen()
    {
        InlogScene.SetActive(false);
        StartScreen.SetActive(true);
    }

    public async void Login()
    {
        UserEmail = UsernameInput.text;
        UserWachtwoord = WachtwoordInput.text;

        IWebRequestReponse webRequestResponse = await UserAPI.Login(new User { Email = UserEmail, Password = UserWachtwoord });

        if (webRequestResponse is WebRequestData<string> dataResponse)
        {
            Debug.Log("Login succes!");
            InlogScene.SetActive(false);
            WereldSelect.SetActive(true);
        }
        else if (webRequestResponse is WebRequestError errorResponse)
        {
            string errorMessage = errorResponse.ErrorMessage;
            Debug.Log("Login error: " + errorMessage);
            InlogErrorTekst.gameObject.SetActive(true);
        }
        else
        {
            throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }
}
