using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using System;
using FMOD.Studio;
using FMODUnity;

public class RaycastControl : MonoBehaviour
{
    //private EventInstance eventInstance;

    //creating a delegate
    public delegate void RaycastEventDelegate();
    public static RaycastEventDelegate RaycastHit01;
    public static RaycastEventDelegate RaycastHit02;
    public static RaycastEventDelegate RaycastHit03;

    public LayerMask triggerLayer01;
    public LayerMask triggerLayer02;
    public LayerMask triggerLayer03;

    bool LastLayer01 = false;
    bool LastLayer02 = true;
    bool LastLayer03 = true;

    bool DisableLayer01 = false;

    private bool startPressedFlag;

    // Flag used in Audiomanager to turn Narrator ON/OFF correctly
    private bool layerOneTrigged = false;
    private bool layerTwoTrigged = false;
    private bool layerThreeTrigged = false;
    #region Properties (public set and private get)

    public bool LayerOneTrigged
    {
        get => layerOneTrigged;
        private set
        {
            layerOneTrigged = value;
        }
    }

    public bool LayerTwoTrigged
    {
        get => layerTwoTrigged;
        private set
        {
            layerTwoTrigged = value;
        }
    }

    public bool LayerThreeTrigged
    {
        get => layerThreeTrigged;
        private set
        {
            layerThreeTrigged = value;
        }
    }
    #endregion

    [SerializeField] private GameManager gameManager;

    Ray ray;

    Vector3 rayDirection;

    private void Update()
    {
        startPressedFlag = gameManager.StartPressedFlag;

        ray = new Ray(transform.position, transform.forward);

        rayDirection = transform.TransformDirection(Vector3.forward) * 20;
        Debug.DrawRay(transform.position, rayDirection, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hitInfo01, maxDistance: 20, layerMask: triggerLayer01))
        {
            ActivateRaycastHit01();
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo02, maxDistance: 20, layerMask: triggerLayer02))
        {
            ActivateRaycastHit02();
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo03, maxDistance: 20, layerMask: triggerLayer03))
        {
            ActivateRaycastHit03();
        }
    }

    public void ActivateRaycastHit01()
    {
        if (RaycastHit01 != null && LastLayer01 != true && DisableLayer01 == false && startPressedFlag == true)
        {
            RaycastHit01();

            LastLayer01 = true;
            LastLayer02 = false;
            LastLayer03 = false;

            DisableLayer01 = true;

            Debug.Log("layer 1 trig");

            // Flag for narrator
            LayerOneTrigged = true;
        }
    }

    public void ActivateRaycastHit02()
    {
        if (RaycastHit02 != null && LastLayer02 != true && startPressedFlag == true)
        {
            RaycastHit02();

            LastLayer01 = false;
            LastLayer02 = true;
            LastLayer03 = false;

            Debug.Log("layer 2 trig");

            // Flag for narrator
            LayerTwoTrigged = true;
        }
    }

    public void ActivateRaycastHit03()
    {
        if (RaycastHit03 != null && LastLayer03 != true && startPressedFlag == true)
        {
            RaycastHit03();

            LastLayer01 = false;
            LastLayer02 = false;
            LastLayer03 = true;

            Debug.Log("layer 3 trig");

            // Flag for narrator
            LayerThreeTrigged = true;
        }
    }
}
