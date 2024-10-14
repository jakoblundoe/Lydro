using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamValueSlider : MonoBehaviour
{
    public string paramName;

    [Range(0.0f, 1.0f)]

    public float mySliderFloat;

    private void Update()
    {
        if (mySliderFloat > 1f)
        {
            mySliderFloat = 1f;
        }

        if (mySliderFloat < 0f)
        {
            mySliderFloat = 0f;
        }

        ChangeParamValue();
    }

    public void ChangeParamValue()
    {
        AudioManager.instance.SetGlobalParameter(paramName, mySliderFloat);
    }
}
