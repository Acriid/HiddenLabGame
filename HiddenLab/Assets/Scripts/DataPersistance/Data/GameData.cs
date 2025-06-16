using Unity.Multiplayer.Center.Common;
using UnityEngine;
//System.Serializable allow you to save the data to/from a string
[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int PlayerHealth;
    public float Slime1Speed;
    public float Slime2Speed;
    public float ImpulseSpeed;
    public bool KeyCard1;
    public bool KeyCard2;
    public bool KeyCard3;
    public bool HasFlashlight;
    public bool CanSplit;
    public bool ReactorOff;
    public int CurrentScene;

    public GameData()
    {
        //Initial game data
        playerPosition.z = 0f;
        playerPosition.y = 0f;
        playerPosition.x = -18f;

        PlayerHealth = 2;
        Slime1Speed = 20f;
        Slime2Speed = 20f;
        ImpulseSpeed = 5f;

        KeyCard1 = false;
        KeyCard2 = false;
        KeyCard3 = false;

        HasFlashlight = false;
        CanSplit = false;
        ReactorOff = false;

        CurrentScene = 2;
    }
}
