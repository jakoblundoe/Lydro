//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using FMODUnity;
//using FMOD.Studio;

//[RequireComponent(typeof(StudioEventEmitter))]

//public class HeadMovementAudioControl : MonoBehaviour
//{
//    // private variables declared --> events
//    private StudioEventEmitter emitter;

//    // private variables declared --> parameters
//    [SerializeField] private string parameterName;

//    // creating private variable for headphonemotionavailability
//    private bool _headphoneMotionAvailable;

//    private void Start()
//    {
//        //emitter = AudioManager.instance.InitializeEventEmitter(FmodEventManager.instance.HeadMovementMain, this.gameObject);
//        //emitter.Play();

//        //store bool in variable -> headphonemotion availability
//        _headphoneMotionAvailable = HeadphoneMotion.IsHeadphoneMotionAvailable();

//        if (_headphoneMotionAvailable)
//        {

//        }

//    }


//    private void SetHeadMovementParameter(string parameterName, float parameterValue)
//    {
//        RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
//    }

//    void Update()
//    {
//        //test if param is working, LeftShift pressed for On
//        if (Input.GetKeyDown(KeyCode.LeftShift))
//        {
//            SetHeadMovementParameter(parameterName, 0.5f);
//        }

//        if (Input.GetKeyUp(KeyCode.LeftShift))
//        {
//            SetHeadMovementParameter(parameterName, 0f);
//        }

//        //if (HeadphoneMotion.IsHeadphoneMotionAvailable)

//        //check if gameObject is moving

//    }


//}
