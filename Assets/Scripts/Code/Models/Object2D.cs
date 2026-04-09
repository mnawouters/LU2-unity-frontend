using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

/**
 * Bijzonderheden wegens beperkingen van JsonUtility:
 * - De id is een string in plaats van een Guid omdat Unity een Guid niet in de Inspector kan tonen. Gelukkig geeft dit geen probleem omdat de backend API de string correct zal interpreteren als een Guid.
*/
[Serializable]
public class Object2D
{
    public Guid ObjGuid { get; set; }

    public float PrefabId { get; set; }

    public float PositionX { get; set; }

    public float PositionY { get; set; }

    public float ScaleX { get; set; }

    public float ScaleY { get; set; }

    public float RotationZ { get; set; }

    public int SortingLayer { get; set; }

    public Guid EnvironmentGuid { get; set; }

    public string ObjName { get; set; }
}