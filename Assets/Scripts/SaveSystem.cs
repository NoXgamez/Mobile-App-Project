using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/save.json";

    public static void Save(PlayerSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static PlayerSaveData Load()
    {
        if (File.Exists(SavePath))
        {
            Debug.Log("Save path: " + Application.persistentDataPath);

            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<PlayerSaveData>(json);
        }
        return new PlayerSaveData();
    }
}