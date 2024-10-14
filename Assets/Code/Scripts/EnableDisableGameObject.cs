using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableGameObject : MonoBehaviour
{
    public void EnableThisGameObject()
    {
        if (gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
        }
    }

    public void DisableThisGameObject()
    {
        if (gameObject.activeSelf == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
