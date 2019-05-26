using System;
using UnityEngine;
[Serializable]
public class Levels
{
    public Level[] levels;
    public static Levels CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Levels>(jsonString);
    }
}

