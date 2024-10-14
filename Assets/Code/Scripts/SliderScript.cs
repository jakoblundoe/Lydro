using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SliderScript : MonoBehaviour
{
    public UnityEvent eventIntSliderClickFeedback;
    public UnityEvent eventFloatSliderClickFeedback;

    [SerializeField] private Slider _slider;
    private int sliderIntValue;
    //private float sliderFloatValue;

    public IntEvent _intEvent;

    public FloatEvent _floatEvent;

    public int SliderIntValue
    {
        get => sliderIntValue;
        private set
        {
            if (sliderIntValue != value) { eventIntSliderClickFeedback.Invoke(); }
            sliderIntValue = value;
        }
    }

    //public float SliderFloatValue
    //{
    //    get => sliderFloatValue;
    //    private set
    //    {
    //        if (sliderFloatValue != value) { eventFloatSliderClickFeedback.Invoke(); Debug.Log("slider float value has changed to; " + sliderFloatValue); }
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            SliderIntValue = (int)v;
            _intEvent.Invoke(SliderIntValue);

            //SliderFloatValue = v;
            //_floatEvent.Invoke(SliderFloatValue);

            //sliderIntValue = (int)v;
            //_intEvent.Invoke(sliderIntValue);

            //sliderFloatValue = v;
            //_floatEvent.Invoke(sliderFloatValue);
        });
    }
}