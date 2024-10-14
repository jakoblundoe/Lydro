using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]

public class RandomTrigger : MonoBehaviour
{
    public string arrayName;
    public string arrayItemName;
    private StudioEventEmitter emitter;

    [SerializeField] float setMinTime;
    [SerializeField] float setMaxTime;

    float timer;
    float randomFloat;

    float debugAccurateTime;

    bool run = false;

    //private void Start()
    //{
    //    StartTimer();
    //    SetTimer(setMinTime);
    //}

    public void StartRandomizer()
    {
        StartTimer();
        SetTimer(setMinTime);
    }

    private void Update()
    {
        if (!run)
            return;

        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, Mathf.Infinity); //makes sure we don't go below 0 in our timer

        if (timer == 0)
        {
            SetTimer(setMinTime);
            AddTime(randomFloat);

            // Trigger event here
            PlaySoundEvent();

            Randomizer();

            debugAccurateTime = setMinTime + randomFloat;
        }
    }


    void PlaySoundEvent()
    {
        if (arrayName != null && arrayItemName != null)
        {
            emitter = AudioManager.instance.InitializeEventEmitter(FmodEventManager.instance.GetEventArrayReference(arrayName, arrayItemName), this.gameObject);
            emitter.Play();
        }
    }


    void Randomizer()
    {
        randomFloat = Random.Range(setMinTime, setMaxTime);
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
}