using System.Collections.Generic;
using Unity.Services.CloudSave.Models;
using UnityEngine;

/// <summary>
/// Contains methods to help manage player's cloud data.
/// </summary>
public static class HelperCloudSave
{
    /// <summary>
    /// Set's variables value from the player's cloud data.
    /// Usually used after calling CloudSaveManager.RetrieveAllData
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetValue<T>(Dictionary<string, Item> data, string key, ref T value)
    {
        // On a new game, the players save data won't have certain keys
        // so we check if the key exists first
        if (data.ContainsKey(key))
        {
            value = data[key].Value.GetAs<T>();
        }
        else
        {
            Debug.Log("<color=#12a182>HelperCloudSave.cs: Key not found: " + key + "</color>");
        }
    }
}
