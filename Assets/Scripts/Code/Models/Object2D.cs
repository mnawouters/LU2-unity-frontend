using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Object2D
{
    [JsonProperty("objGuid")]
    public string Id;

    [JsonProperty("environmentGuid")]
    public string EnvironmentId;

    [JsonProperty("prefabId")]
    public int PrefabID;

    public float PositionX;
    public float PositionY;
    public float ScaleX;
    public float ScaleY;
    public float RotationZ;
    public int SortingLayer;
    public string ObjName;
}
