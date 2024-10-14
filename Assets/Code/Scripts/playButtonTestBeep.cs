using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

public class playButtonTestBeep : MonoBehaviour
{
    public string arrayName;
    public string arrayItemName;

    private EventInstance eventInstance;

    void Start()
    {

    }

    private void ExecuteAudioEvent()
    {
        //Debug.Log("debug on button click");
        if (arrayName != null && arrayItemName != null)
        {
            eventInstance = AudioManager.instance.CreateEventInstance(FmodEventManager.instance.GetEventArrayReference(arrayName, arrayItemName), this.transform.position);
            eventInstance.start();
        }
    }

    public void OnButtonClick()
    {
        ExecuteAudioEvent();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        
    }

}
