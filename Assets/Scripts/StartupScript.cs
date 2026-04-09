using JetBrains.Annotations;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class StartupScript : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject InlogScene;
    public GameObject RegisterScene;
    public GameObject WereldSelectScene;
    public GameObject InWorldScene;

    public void Start()
    {
        StartScreen.SetActive(true);
        InlogScene.SetActive(false);
        RegisterScene.SetActive(false);
        WereldSelectScene.SetActive(false);
        InWorldScene.SetActive(false);
    }
}
