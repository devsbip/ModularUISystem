using System.IO;
using UnityEngine;

/// <summary>
/// A generic and reusable system for serializing and deserializing data to JSON.
/// </summary>
public class SaveSystem : MonoBehaviour
{
    /// <summary>
    /// Saves data of type T to a JSON file.
    /// </summary>
    public static void Save<T>(T data, string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
            Debug.Log($"[SaveSystem] Successfully saved to: {path}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveSystem] Failed to save data: {e.Message}");
        }
    }

    /// <summary>
    /// Loads data of type T from a JSON file. Returns a new instance if file not found.
    /// </summary>
    public static T Load<T>(string fileName) where T : new()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                Debug.Log($"[SaveSystem] Successfully loaded from: {path}");
                return JsonUtility.FromJson<T>(json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Failed to load data: {e.Message}");
            }
        }

        Debug.LogWarning($"[SaveSystem] File not found or corrupted. Creating new Instance of {typeof(T)}.");
        return new T(); // Return default values if it fails.
    }
}
