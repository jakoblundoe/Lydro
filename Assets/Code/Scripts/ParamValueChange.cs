using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamValueChange : MonoBehaviour
{
    public string paramName;
    public float paramFloatValue;

    private void Update()
    {
        if (paramFloatValue > 1f)
        {
            paramFloatValue = 1f;
        }

        if (paramFloatValue < 0f)
        {
            paramFloatValue = 0f;
        }

        ChangeParamValue();
    }

    public void ChangeParamValue()
    {
        AudioManager.instance.SetGlobalParameter(paramName, paramFloatValue);
    }
}
