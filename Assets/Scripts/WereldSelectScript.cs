using UnityEngine;

public class WereldSelectScript : MonoBehaviour
{
    public GameObject TargetWorld;
    public GameObject CurrentWorld;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Worldselect script started");
    }

    
    public void Worldselect()
    {
        TargetWorld.SetActive(true);
        CurrentWorld.SetActive(false);
    }
}
