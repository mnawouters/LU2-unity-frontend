using System;
using Newtonsoft.Json;

[Serializable]
public class Environment2D
{
    [JsonProperty("envGuid")]
    public string Id;

    public string Name;

    public int MaxLenght;

    public int MaxHeight;
}
