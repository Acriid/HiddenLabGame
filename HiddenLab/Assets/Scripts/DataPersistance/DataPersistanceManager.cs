using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections.Generic;

public class DataPersistanceManager : MonoBehaviour
{   
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    /*Makes it so that only one instance of this script can be active and that it can
     only be change locally*/
    public static DataPersistanceManager instance {get ; private set;}
    public GameData gameData;
    private List<iDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;


    private void Awake()
    {
        //Checks if it already exists
        if (instance != null)
        {
            Debug.LogError("There is more than one instance of the datapersistancemanager");
        }
        instance = this;

        //Loads the data
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    //Makes a new game
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        //Gets data to load
        this.gameData = dataHandler.Load();
        if(this.gameData == null)
        {
            Debug.Log("Making new game due to no game data being found.");
            NewGame();
        }

        foreach(iDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        //Saves data to file
        foreach(iDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }
    //Gets all scripts that use the save/load system
    private List<iDataPersistence> FindAllDataPersistenceObjects()
    {
        /*To use the save/load system the script needs to be a MonoBehaivour aka
        be attatched to a gameobject*/
        IEnumerable<iDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
        .OfType<iDataPersistence>();
        return new List<iDataPersistence>(dataPersistenceObjects);
    }

    public void DeleteSave()
    {
        this.dataHandler.DeleteSave();
    }
}
