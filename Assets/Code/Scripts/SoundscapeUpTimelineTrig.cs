using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoundscapeUpTimelineTrig : MonoBehaviour
{
    // trigger conditions
    public bool triggerOnlyOnce;

    // var to monitor if first playback has happened
    private bool stopSecondPlayback = false;

    private PlayableDirector timeline;
    //public GameObject controlPanel;

    // flag to know if timeline GameObject has been disabled from menu
    private bool ToggleNarratorFlag = false;
    private bool TimelineHasBeenActivated = false;

    public bool DisableNarrator = false;

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
        timeline.played += Timeline_Played;
        timeline.stopped += Timeline_Stopped;
    }

    private void OnEnable()
    {
        timeline.Stop();
        RaycastControl.RaycastHit02 += StartTimeline;
        RaycastControl.RaycastHit03 += StopTimeline;
        ToggleNarratorFlag = false;
    }

    private void OnDisable()
    {
        RaycastControl.RaycastHit02 -= StartTimeline;
        RaycastControl.RaycastHit03 -= StopTimeline;
    }

    private void Timeline_Played(PlayableDirector obj)
    {

    }

    private void Timeline_Stopped(PlayableDirector obj)
    {

    }

    private void StartTimeline()
    {
        if (stopSecondPlayback == false && !DisableNarrator)
        {
            timeline.Stop();
            timeline.Play();
            TimelineHasBeenActivated = true;

            if (triggerOnlyOnce == true)
            {
                stopSecondPlayback = true;
            }
        }
    }

    private void StopTimeline()
    {
        timeline.Stop();
    }

    // stops the timeline (resets conditions) when narrator toggle off is initiated
    public void ResetStopTimeline()
    {
        if (ToggleNarratorFlag == false && TimelineHasBeenActivated == true)
        {
            timeline.Stop();
            ToggleNarratorFlag = true;
        }
    }

    // starts the timeline (resets conditions) when narrator toggle on is initiated
    public void ResetStartTimeline()
    {
        if (ToggleNarratorFlag == true && !DisableNarrator)
        {
            timeline.Play();
            ToggleNarratorFlag = false;
        }
    }
    public void DisableNarratorTimeline()
    {
        timeline.Stop();
        DisableNarrator = true;
    }

    public void EnableNarratorTimeline()
    {
        DisableNarrator = false;
    }
}