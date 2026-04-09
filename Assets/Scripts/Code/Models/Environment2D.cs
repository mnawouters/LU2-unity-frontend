using System;
using Unity.VisualScripting.Dependencies.Sqlite;

/**
 * Bijzonderheden wegens beperkingen van JsonUtility:
 * - De id is een string in plaats van een Guid omdat Unity een Guid niet in de Inspector kan tonen. Gelukkig geeft dit geen probleem omdat de backend API de string correct zal interpreteren als een Guid.
 */
[Serializable]
public class Environment2D
{
    public Guid EnvGuid { get; set; }

    public string Name { get; set; }

    public int MaxHeight { get; set; }

    public int MaxLenght { get; set; }

    public string UserId { get; set; }
}
