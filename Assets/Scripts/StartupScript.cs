using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
