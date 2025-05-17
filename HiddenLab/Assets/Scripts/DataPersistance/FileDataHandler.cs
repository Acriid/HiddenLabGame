using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    //Directory where data is stored
    private string dataDirPath = "";
    //Save file name
    private string dataFileName = "";
    //Contructer to initialize script
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        //Gets full directory path
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //Load from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath,FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Something went wrong when trying to load from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        //Gets full directory path
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //Gets Makes the Directory
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);
            //Write to file
            using(FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        //Error Handling
        catch (Exception e)
        {
            Debug.LogError("Something went wrong with the file" + fullPath +"\n" + e);
        }
    }

    public void DeleteSave()
    {
        //Gets full directory path
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        File.Delete(fullPath);
    }
}
