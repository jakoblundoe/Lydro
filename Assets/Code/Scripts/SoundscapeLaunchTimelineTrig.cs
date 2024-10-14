using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoundscapeLaunchTimelineTrig : MonoBehaviour
{
    private PlayableDirector timeline;
    //public GameObject controlPanel;

    // flag to know if timeline GameObject has been disabled from menu
    private bool ToggleNarratorFlag = false;

    public bool DisableNarrator = false;

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
        timeline.played += Timeline_Played;
        timeline.stopped += Timeline_Stopped;
    }

    private void OnEnable()
    {
        RaycastControl.RaycastHit01 += StopTimeline;
        RaycastControl.RaycastHit02 += StopTimeline;
        RaycastControl.RaycastHit03 += StopTimeline;
        ToggleNarratorFlag = false;
    }

    private void OnDisable()
    {
        RaycastControl.RaycastHit01 -= StopTimeline;
        RaycastControl.RaycastHit02 -= StopTimeline;
        RaycastControl.RaycastHit03 -= StopTimeline;
    }

    private void Timeline_Played(PlayableDirector obj)
    {

    }

    private void Timeline_Stopped(PlayableDirector obj)
    {

    }

    public void StopTimeline()
    {
        timeline.Stop();
    }

    // stops the timeline (resets conditions) when narrator toggle off is initiated
    public void ResetStopTimeline()
    {
        if (ToggleNarratorFlag == false)
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
