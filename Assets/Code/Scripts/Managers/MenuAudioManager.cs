using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UI;

public class MenuAudioManager : MonoBehaviour
{
    // creating lists to store event instances
    private List<EventInstance> eventInstances;

    public static MenuAudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Audio Manager in the same scene");
        }
        instance = this;

        // initialize eventInstance list as new list
        eventInstances = new List<EventInstance>();
    }

    public EventInstance CreateEventInstance(EventReference eventReference, Vector3 worldPos)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    // method for stopping all narrator timelines
    public void PauseAllFmodEvents()
    {
        // Pause all EventInstances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.setPaused(true);
        }
    }

    public void ResumeAllFmodEvents()
    {
        // Resume all EventInstances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.setPaused(false);
        }
    }

    // method for stop and release of all playing events in list "eventInstances"
    private void CleanUp()
    {
        // stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    // calls the CleanUp method upon destroy
    private void OnDestroy()
    {
        CleanUp();
    }
}