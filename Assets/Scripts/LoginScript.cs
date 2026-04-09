using UnityEditor;
using UnityEngine;

public class MyCustomComponent : MonoBehaviour
{
    //public string message;
    //public int myNumber;
    public GameObject loginObject;
    public GameObject worldObject;
    //public Canvas someCanvas;
    public TMPro.TMP_InputField gebruiker;
    public TMPro.TMP_InputField wachtwoord;
    //public TMPro.TMP_Text someTextField;

    void Start()
    {
        Debug.Log("Hello. I've started!");
    }

    public void DoSomething()
    {
        //Debug.Log(message);
        //someGameObject.SetActive(false);

        //string inputText = someInputField.text;
        //someTextField.text = inputText;

        string gebruikerInput = gebruiker.text;
        string wachtwoordInput = wachtwoord.text;

        if (gebruikerInput == "admin" && wachtwoordInput == "admin")
        {
            loginObject.SetActive(false);
            worldObject.SetActive(true);
        }
    }

}
