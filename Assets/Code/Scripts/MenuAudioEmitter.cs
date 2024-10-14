using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MenuAudioEmitter : MonoBehaviour
{
    public string arrayName;
    public string arrayItemName;

    public string localParamName;
    public float localParamValue;

    private EventInstance eventInstance;

    [SerializeField]
    private float delayTime;

    private float currentTimerValue;

    private BasicTimer basicTimer;

    // check if gameObject is used as OnEnable or OnCall or onStart
    [SerializeField]
    private bool initiateOnEnable = false;

    private bool timerIsAdded = false;

    [SerializeField]
    private bool initiateOnStart = false;

    // flag to check if sound has been initiated
    private bool soundInitiated = false;

    private Transform previousTransform;

    private float previousParamValue;

    private void Start()
    {
        previousTransform = transform;

        previousParamValue = localParamValue;

        if (initiateOnStart)
        {
            PlaySoundEvent();
        }
    }

    private void Update()
    {
        if (eventInstance.isValid())
        {
            if (transform.hasChanged)
            {
                eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(this.transform));
                transform.hasChanged = false;
            }

            if (localParamValue != previousParamValue)
            {
                SetLocalParameter();
                previousParamValue = localParamValue;
            }
        }

        // continues to monitor the BasicTimer instance
        if (timerIsAdded == true)
        {
            if (basicTimer.run)
            {
                currentTimerValue = basicTimer.timer;

                if (currentTimerValue <= 0)
                {
                    basicTimer.StopTimer();
                    basicTimer.SetTimer(delayTime);
                    PlaySoundEvent();

                    if (eventInstance.isValid())
                    {
                        SetLocalParameter();
                    }
                }

            }
        }
        previousTransform = transform;
    }

    private void OnEnable()
    {
        if (soundInitiated == false && initiateOnEnable == true)
        {
            // adds the BasicTimer script to the gameObject
            if (timerIsAdded != true)
            {
                basicTimer = gameObject.AddComponent<BasicTimer>();
                timerIsAdded = true;
            }

            // checks if the basic timer is used or not - if not, it plays the soundevent
            if (delayTime != 0f)
            {
                basicTimer.SetTimer(delayTime);
                basicTimer.StartTimer();
            }
            else
            {
                PlaySoundEvent();

                if (eventInstance.isValid())
                {
                    SetLocalParameter();
                }
            }
        }
    }

    private void OnDisable()
    {
        StopSoundEvent();
    }

    public void InitiateSoundEvent()
    {
        if (soundInitiated == false && initiateOnEnable == false)
        {

            // adds the BasicTimer script to the gameObject
            basicTimer = gameObject.AddComponent<BasicTimer>();
            timerIsAdded = true;

            // checks if the basic timer is used or not - if not, it plays the soundevent
            if (delayTime != 0f)
            {
                basicTimer.SetTimer(delayTime);
                basicTimer.StartTimer();
            }
            else
            {
                PlaySoundEvent();

                if (eventInstance.isValid())
                {
                    SetLocalParameter();
                }
            }
        }
    }

    public void PlaySoundEvent()
    {
        //Debug.Log("PlaySoundEvent called");
        if (arrayName != null && arrayItemName != null)
        {
            Vector3 worldPos = this.transform.position;
            eventInstance = MenuAudioManager.instance.CreateEventInstance(FmodEventManager.instance.GetEventArrayReference(arrayName, arrayItemName), worldPos);
            eventInstance.start();
            soundInitiated = true;
        }
        else
        {
            Debug.LogError("FmodEventManager instance not found");
        }
    }

    public void StopSoundEvent()
    {
        //Debug.Log("StopSoundEvent called");
        if (soundInitiated == true && eventInstance.isValid())
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
            soundInitiated = false;
        }
    }

    public void SetLocalVolParam(float _float)
    {
        if (localParamName != null && eventInstance.isValid())
        {
            eventInstance.setParameterByName(localParamName, _float);
        }
    }

    public void SetLocalParameter()
    {
        if (localParamName != null && eventInstance.isValid())
        {
            eventInstance.setParameterByName(localParamName, localParamValue);
        }
    }

    public void SetNextKeyOff()
    {
        eventInstance.keyOff();
    }
}