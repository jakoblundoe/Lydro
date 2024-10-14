using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Range(0, 10)]
    private int masterVolume = 6;

    [Range(1, 3)]
    private int narratorVolume = 2;

    [Range(0, 10)]
    [SerializeField] private int SFX_VolumeSlider = 8;

    [Range(0, 10)]
    [SerializeField] private int MUSIC_VolumeSlider = 7;

    // creating private bus variables
    //private Bus masterBus;

    // reference to NarratorTimeline GameObjects
    public GameObject narratorLaunch;
    public GameObject narratorAwake;
    public GameObject narratorUp;
    public GameObject narratorDown;

    // Get Raycasts triggers to turn Narrator ON/OFF correctly
    [SerializeField] private RaycastControl raycastControl;
    private bool layerOneTrigged = false;
    private bool layerTwoTrigged = false;
    private bool layerThreeTrigged = false;

    // creating lists to store event instances
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    public int MasterVolume
    {
        get => masterVolume;
        private set
        {
            masterVolume = value;
        }
    }

    public int NarratorVolume
    {
        get => narratorVolume;
        private set
        {
            narratorVolume = value;
        }
    }

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Audio Manager in the same scene");
        }
        instance = this;

        // initialize eventInstance list as new list
        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        // attach bus variables with corresponding busses in fmod
        //masterBus = RuntimeManager.GetBus("Bus:/");
    }

    private void Update()
    {
        RuntimeManager.StudioSystem.setParameterByName("SFX_VOLUME", SFX_VolumeSlider);
        RuntimeManager.StudioSystem.setParameterByName("MUSIC_VOLUME", MUSIC_VolumeSlider);
    }

    public void SetMasterVolume(int MasterVolume)
    {
        RuntimeManager.StudioSystem.setParameterByName("paramMasterVolume", MasterVolume);
        //Debug.Log("set master volume to " + MasterVolume);
    }

    public void SetNarratorVolume(int NarratorVolume)
    {
        RuntimeManager.StudioSystem.setParameterByName("NarratorBusVolume", NarratorVolume);
        RuntimeManager.StudioSystem.setParameterByName("NarratorTickVolBus", NarratorVolume);
        //Debug.Log("set narrator volume to " + NarratorVolume);
    }



    public EventInstance CreateEventInstance(EventReference eventReference, Vector3 worldPos)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    public void ToggleNarratorOnOff(bool _bool)
    {
        layerOneTrigged = raycastControl.LayerOneTrigged;
        layerTwoTrigged = raycastControl.LayerTwoTrigged;
        layerThreeTrigged = raycastControl.LayerThreeTrigged;

        if (!_bool)
        {
            SetNarratorOff();
        }
        else
        {
            SetNarratorOn();
        }
    }

    public void SetNarratorOn()
    {
        if (layerOneTrigged)
        {
            narratorAwake.GetComponent<SoundscapeAwakeTimelineTrig>().ResetStartTimeline();
        }
        else
        {
            narratorLaunch.GetComponent<SoundscapeLaunchTimelineTrig>().ResetStartTimeline();
        }

        if (layerOneTrigged && layerTwoTrigged)
        {
            narratorUp.GetComponent<SoundscapeUpTimelineTrig>().ResetStartTimeline();
        }

        if (layerOneTrigged && layerTwoTrigged && layerThreeTrigged)
        {
            narratorDown.GetComponent<SoundscapeDownTimelineTrig>().ResetStartTimeline();
        }

        narratorAwake.GetComponent<SoundscapeAwakeTimelineTrig>().EnableNarratorTimeline();
        narratorLaunch.GetComponent<SoundscapeLaunchTimelineTrig>().EnableNarratorTimeline();
        narratorUp.GetComponent<SoundscapeUpTimelineTrig>().EnableNarratorTimeline();
        narratorDown.GetComponent<SoundscapeDownTimelineTrig>().EnableNarratorTimeline();

        //Debug.Log("Narrator is ON");
    }

    public void SetNarratorOff()
    {
        if (layerOneTrigged && !layerTwoTrigged && !layerThreeTrigged)
        {
            narratorAwake.GetComponent<SoundscapeAwakeTimelineTrig>().ResetStopTimeline();
        }
        else
        {
            narratorLaunch.GetComponent<SoundscapeLaunchTimelineTrig>().ResetStopTimeline();
        }

        if (layerOneTrigged && layerTwoTrigged && !layerThreeTrigged)
        {
            narratorUp.GetComponent<SoundscapeUpTimelineTrig>().ResetStopTimeline();
        }

        if (layerOneTrigged && layerTwoTrigged && layerThreeTrigged)
        {
            narratorDown.GetComponent<SoundscapeDownTimelineTrig>().ResetStopTimeline();
            //Debug.Log("NarratorDown should be set to off");
        }

        narratorAwake.GetComponent<SoundscapeAwakeTimelineTrig>().DisableNarratorTimeline();
        narratorLaunch.GetComponent<SoundscapeLaunchTimelineTrig>().DisableNarratorTimeline();
        narratorUp.GetComponent<SoundscapeUpTimelineTrig>().DisableNarratorTimeline();
        narratorDown.GetComponent<SoundscapeDownTimelineTrig>().DisableNarratorTimeline();

        //Debug.Log("Narrator is OFF");
    }

    public void SetGlobalParameter(string parameterName, float parameterValue)
    {
        RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
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

        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    // calls the CleanUp method upon destroy
    private void OnDestroy()
    {
        CleanUp();
    }
}