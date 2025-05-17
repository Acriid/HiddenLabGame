using UnityEngine;

public interface iDataPersistence
{
    //No refrence meaning the script only reads
    void LoadData(GameData data);
    //the refrence allows the script to modify
    void SaveData(ref GameData data);
}
