using UnityEngine;
using System.Collections.Generic;

public static class RespawnManager
{
    private static readonly List<Resettable> resettables = new();

    public static void Register(Resettable r) => resettables.Add(r);
    public static void Unregister(Resettable r) => resettables.Remove(r);

    public static void ResetAll()
    {
        foreach (var r in resettables)
            if (r != null) r.ResetToStart();
    }
}