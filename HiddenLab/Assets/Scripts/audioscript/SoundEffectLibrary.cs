using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=rAX_r0yBwzQ
public class SoundEffectLibrary : MonoBehaviour
{
    // Group of sound effects set in the Inspector
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;

    // A dictionary to map group names to a list of audio clips
    private Dictionary<string, List<AudioClip>> soundDictionary;

    private void Awake()
    {
        // Set up the dictionary when the object awakens
        InitializeSoundDictionary();
    }

    // Builds the dictionary from the serialized array
    private void InitializeSoundDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();

        // Loop through each group and add it to the dictionary
        foreach (SoundEffectGroup group in soundEffectGroups)
        {
            soundDictionary[group.name] = group.audioClips;
        }
    }

    // Returns a random audio clip from the group with the given name
    public AudioClip GetRandomClip(string groupName)
    {
        // Check if the dictionary has the requested group
        if (soundDictionary.TryGetValue(groupName, out List<AudioClip> clips))
        {
            // Return a random clip if the list is not empty
            if (clips.Count > 0)
            {
                return clips[UnityEngine.Random.Range(0, clips.Count)];
            }
        }

        // If the group doesn't exist or has no clips, return null
        return null;
    }

    // Called before the first frame update (optional in this case)
    void Start()
    {
    }

    // Called once per frame (optional in this case)
    void Update()
    {
    }
}

// A serializable struct to represent a named group of audio clips
[System.Serializable]
public struct SoundEffectGroup
{
    public string name;
    public List<AudioClip> audioClips;
}