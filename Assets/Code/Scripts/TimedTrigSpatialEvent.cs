using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TimedTrigSpatialEvent : MonoBehaviour
{
    public string arrayName;
    public string arrayItemName;

    private StudioEventEmitter emitter;

    [SerializeField] float setTimer;

    float timer;

    bool run = false;

    private void Update()
    {
        if (!run)
            return;

        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, Mathf.Infinity); //makes sure we don't go below 0 in our timer

        if (timer == 0)
        {
            SetTimer(setTimer);

            //if (emitter.IsPlaying = true)
            //{

            //}
            PlaySoundEvent();
        }
    }

    private void OnEnable()
    {
        StartTimer();
        SetTimer(setTimer);

        RaycastControl.RaycastHit03 += StartTimer;
        RaycastControl.RaycastHit02 += StopTimer;
    }

    private void OnDisable()
    {
        RaycastControl.RaycastHit03 -= StartTimer;
        RaycastControl.RaycastHit03 -= StopTimer;
        StopSoundEvent();
    }

    void StartTimer()
    {
        run = true;
    }

    void StopTimer()
    {
        run = false;
    }

    void AddTime(float seconds)
    {
        timer += seconds;
    }

    void SetTimer(float seconds)
    {
        timer = seconds;
    }

    void PlaySoundEvent()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FmodEventManager.instance.GetEventArrayReference(arrayName, arrayItemName), this.gameObject);
        emitter.Play();
    }

    void StopSoundEvent()
    {
        emitter.Stop();
    }
}
