using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save()
    {
        BinaryFormatter binaryFormatter = new();
        string path = Application.persistentDataPath + "/SaveData.txt";
        FileStream stream = new(path, FileMode.Create);

        SaveData data = new();

        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/SaveData.txt";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new();
            FileStream stream = new(path, FileMode.Open);

            SaveData data = binaryFormatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Отсутствует файл сохранения");
            return null;
        }
    }

}
