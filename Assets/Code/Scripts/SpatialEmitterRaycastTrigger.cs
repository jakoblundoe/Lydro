using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using FMOD.Studio;

[RequireComponent(typeof(StudioEventEmitter))]

public class SpatialEmitterRaycastTrigger : MonoBehaviour
{
    public string arrayName;
    public string arrayItemName;

    // private variables declared
    private StudioEventEmitter emitter;

    private void OnEnable()
    {
        RaycastControl.RaycastHit02 += PlaySoundEvent;
    }

    private void OnDisable()
    {
        RaycastControl.RaycastHit02 -= PlaySoundEvent;
    }
    
    private void PlaySoundEvent()
     {
        if (arrayName != null && arrayItemName != null)
        {
            emitter = AudioManager.instance.InitializeEventEmitter(FmodEventManager.instance.GetEventArrayReference(arrayName, arrayItemName), this.gameObject);
            emitter.Play();
        }
     }
}
