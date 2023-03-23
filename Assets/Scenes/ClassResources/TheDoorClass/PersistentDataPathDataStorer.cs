using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistentDataPathDataStorer
{
    private const string SAVE_FOLDER_NAME = "saves";

    private string BuildFolderPath()
    {
        string rootPath = Application.persistentDataPath;
        string folderPath = Path.Combine(rootPath, SAVE_FOLDER_NAME);

        return folderPath;
    }

    private string BuildFilePath(string fileName)
    {
        string folderPath = BuildFolderPath();
        string filePath = Path.Combine(folderPath, $"{fileName}.json");

        return filePath;
    }

     public bool HasData(string fileName)
    {
        return File.Exists(BuildFilePath(fileName));
    }

    public string RetrieveData(string fileName)
    {
        string result = string.Empty;

        using (StreamReader reader = new StreamReader(BuildFilePath(fileName)))
        {
            result = reader.ReadToEnd();
        }

        /*
        StreamReader reader = new StreamReader(BuildFilePath(fileName));
        result = reader.ReadToEnd();
        reader.Close();
        */

        return result;
    }

    public IEnumerator StoreData(string fileName, SaveSystem saveSystem, string data)
    {
        string folderPath = BuildFolderPath();
        string filePath = BuildFilePath(fileName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write(data);
            writer.Flush();
            writer.Close();
        }

        yield break;
    }

}
