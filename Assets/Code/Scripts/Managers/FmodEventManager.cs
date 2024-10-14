using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

// Arrays containing arrays
[Serializable]
public class FmodEventArrays
{
    public string arrayName;
    public FmodEvents[] fmodEventArrays;
}

// Individual arrays containing items with eventreferences
[Serializable]
public class FmodEvents
{
    public string eventItemName;
    public EventReference eventReference;
}

public class FmodEventManager : MonoBehaviour
{
    public FmodEventArrays[] FmodAllArrays;

    public static FmodEventManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one FMOD Events scripts in the same scene");
        }
        instance = this;
    }

    public EventReference GetEventArrayReference(string arrayName, string arrayItemName)
    {
        FmodEvents[] fmodEvents = Array.Find(FmodAllArrays, f => f.arrayName == arrayName).fmodEventArrays;
        return Array.Find(fmodEvents, e => e.eventItemName == arrayItemName).eventReference;
    }
}