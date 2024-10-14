using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(RotationSpeed))]

public class HeadMotionAudioControl : MonoBehaviour
{
    // declaring variables for accessing arrays and the eventreferences inside
    public string arrayName;
    public string arrayItemName;

    // declaring serializefield variables for setGlobalParameter arguments
    [SerializeField] private string parameterName;

    //private float parameterValue;

    private RotationSpeed getRotationSpeed;

    // declare private string for event that needs to be called
    private EventInstance headMotionEvent;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (arrayName != null && arrayItemName != null)
        {
            getRotationSpeed = GetComponent<RotationSpeed>();
            Vector3 worldPos = this.transform.position;
            headMotionEvent = AudioManager.instance.CreateEventInstance(FmodEventManager.instance.GetEventArrayReference(arrayName, arrayItemName), worldPos);
            headMotionEvent.start();
        }
    }

    private void SetHeadMotionParam(string parameterName, float parameterValue)
    {
        headMotionEvent.setParameterByName(parameterName, parameterValue);
    }

    // Update is called once per frame
    void Update()
    {
        SetHeadMotionParam(parameterName, getRotationSpeed.rotationSpeed);
    }

}
