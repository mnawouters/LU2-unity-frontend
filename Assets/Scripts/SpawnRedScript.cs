using UnityEngine;

public class SpawnRedScript : MonoBehaviour
{
    public GameObject Red;
    public int prefabID;

    public void Spawn()
    {
        GameObject gespawnd = Instantiate(Red, Vector3.zero, Quaternion.identity, WorldManager.Instance.CurrentWorld.transform);

        Object2D newObject = new Object2D
        {
            EnvironmentId = WorldManager.Instance.CurrentWorldId,
            PrefabID = prefabID,
            PositionX = 0,
            PositionY = 0,
            ScaleX = gespawnd.transform.localScale.x,
            ScaleY = gespawnd.transform.localScale.y,
            RotationZ = 0,
            SortingLayer = 1
        };
        WorldManager.Instance.RegistreerObject(gespawnd);


        Draggable draggable = gespawnd.GetComponent<Draggable>();
        if (draggable != null)
        {
            draggable.objectData = newObject;
            draggable.apiClient = WorldManager.Instance.object2DApiClient;
            draggable.CreateObject2D();
        }

        Debug.Log("Red gespawned");
    }
}
