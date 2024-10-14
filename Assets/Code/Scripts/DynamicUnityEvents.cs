using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class VoidEvent : UnityEvent { }
[Serializable] public class BoolEvent : UnityEvent<bool> { }
[Serializable] public class StringEvent : UnityEvent<string> { }
[Serializable] public class IntEvent : UnityEvent<int> { }
[Serializable] public class FloatEvent : UnityEvent<float> { }

public class DynamicUnityEvents : MonoBehaviour
{
    //[SerializeField] VoidEvent voidEvent = null;
    //[SerializeField] BoolEvent boolEvent = null;
    //[SerializeField] StringEvent stringEvent = null;
    //[SerializeField] IntEvent intEvent = null;
    //[SerializeField] FloatEvent floatEvent = null;

    //public void OnVoidEvent()
    //{
    //    Debug.Log("OnVoidEvent");
    //}

    //public void OnBoolEvent(bool value)
    //{
    //    Debug.Log($"OnBoolEvent{value}");
    //}

    //public void OnStringEvent(string value)
    //{
    //    Debug.Log($"OnStringEvent {value}");
    //}

    //public void OnIntEvent(int value)
    //{
    //    Debug.Log($"OnIntEvent {value}");
    //}

    //public void OnFloatEvent(float value)
    //{
    //    Debug.Log($"OnFloatEvent {value}");
    //}
}
