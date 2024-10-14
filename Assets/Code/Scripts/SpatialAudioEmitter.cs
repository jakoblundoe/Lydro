using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(StudioEventEmitter))]

public class SpatialAudioEmitter : MonoBehaviour
{
    public string arrayName;
    public string arrayItemName;

    public string localParamName;
    public float localParamValue;

    [SerializeField]
    private float delayTime;

    private float currentTimerValue;

    private StudioEventEmitter emitter;

    private BasicTimer basicTimer;

    private void Update()
    {
        if (basicTimer.run)
        {
            currentTimerValue = basicTimer.timer;

            if (currentTimerValue <= 0)
            {
                basicTimer.StopTimer();
                basicTimer.SetTimer(delayTime);
                PlaySoundEvent();
            }
        }

        if (localParamName != null && emitter != null)
        {
            SetLocalParameter();
        }
    }

    private void OnEnable()
    {
        basicTimer = gameObject.AddComponent<BasicTimer>();

        if (delayTime != 0f)
        {
            basicTimer.SetTimer(delayTime);
            basicTimer.StartTimer();
        }
        else
        {
            PlaySoundEvent();
        }
    }

    private void OnDisable()
    {
        if (emitter != null)
        {
            StopSoundEvent();
        }
    }

    private void PlaySoundEvent()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FmodEventManager.instance.GetEventArrayReference(arrayName, arrayItemName), this.gameObject);
        emitter.Play();
    }

    private void StopSoundEvent()
    {
        if (emitter != null)
        {
            emitter.Stop();
        }
    }

    private void SetLocalParameter()
    {
        emitter.SetParameter(localParamName, localParamValue);
    }
}