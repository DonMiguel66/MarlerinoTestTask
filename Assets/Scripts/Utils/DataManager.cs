using System.IO;
using UnityEngine;

public class DataManager<T> : IData<T>
{
    public void SaveToJson(T data, string path = null)
    {
        var savedText = JsonUtility.ToJson(data);
        Debug.Log(savedText);
        File.WriteAllText(path, savedText);
    }

    public T LoadFromJson(string path = null)
    {
        var readedText = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(readedText);
    }
}