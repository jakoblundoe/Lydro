using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicTimer : MonoBehaviour
{
    private float setTimer;

    public float timer;

    public bool run = false;

    private void Update()
    {
        if (!run)
            return;

        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, Mathf.Infinity); //makes sure we don't go below 0 in our timer

        if (timer == 0)
        {
            //StopTimer();
            //SetTimer(setTimer);
        }
    }

    public void StartTimer()
    {
        run = true;
    }

    public void StopTimer()
    {
        run = false;
    }

    public void AddTime(float seconds)
    {
        timer += seconds;
    }

    public void SetTimer(float seconds)
    {
        timer = seconds;
    }
}