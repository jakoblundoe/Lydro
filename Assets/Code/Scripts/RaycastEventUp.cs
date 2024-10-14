using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class RaycastEventUp : MonoBehaviour
{
    public string arrayName;
    public string arrayItemName;

    public string globalParamName;
    public float globalParamValue;

    private EventInstance eventInstance;

    [SerializeField]
    private float delayTime;

    private float currentTimerValue;

    private BasicTimer basicTimer;

    private void Update()
    {
        if (eventInstance.isValid())
        {
            eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(this.transform));
        }

        // continues to monitor the BasicTimer instance
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
    }

    private void OnEnable()
    {
        // adds the BasicTimer script to the gameObject
        basicTimer = gameObject.AddComponent<BasicTimer>();

        RaycastControl.RaycastHit02 += StartTimedSoundEvent;
        RaycastControl.RaycastHit02 += SetGlobalParameter;
        RaycastControl.RaycastHit03 += StopSoundEvent;
    }

    private void OnDisable()
    {
        RaycastControl.RaycastHit02 -= StartTimedSoundEvent;
        RaycastControl.RaycastHit02 -= SetGlobalParameter;
        RaycastControl.RaycastHit03 -= StopSoundEvent;

        StopSoundEvent();
    }

    private void StartTimedSoundEvent()
    {
        // checks if the basic timer is used or not - if not, it plays the soundevent
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

    private void PlaySoundEvent()
    {
        if (arrayName != null && arrayItemName != null)
        {
            Vector3 worldPos = this.transform.position;
            eventInstance = AudioManager.instance.CreateEventInstance(FmodEventManager.instance.GetEventArrayReference(arrayName, arrayItemName), worldPos);
            eventInstance.start();
        }
        else
        {
            Debug.LogError("FmodEventManager instance not found");
        }
    }

    private void StopSoundEvent()
    {
        if (eventInstance.isValid())
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
        }
    }

    private void SetGlobalParameter()
    {
        AudioManager.instance.SetGlobalParameter(globalParamName, globalParamValue);
    }
}