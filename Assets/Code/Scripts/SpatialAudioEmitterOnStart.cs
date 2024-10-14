using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]

public class SpatialAudioEmitterOnStart : MonoBehaviour
{
    public string arrayName;
    public string arrayItemName;

    // private variables declared
    private StudioEventEmitter emitter;

    private void Start()
    {
        if (arrayName != null && arrayItemName != null)
        {
            emitter = AudioManager.instance.InitializeEventEmitter(FmodEventManager.instance.GetEventArrayReference(arrayName, arrayItemName), this.gameObject);
            emitter.Play();
        }
    }

}
