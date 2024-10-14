using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using FMODUnity;
using UnityEngine;
using FMOD.Studio;

/// <summary>
/// A simple free camera to be added to a Unity game object.
/// 
/// Keys:
///	wasd / arrows	- movement
///	q/e 			- up/down (local space)
///	r/f 			- up/down (world space)
///	pageup/pagedown	- up/down (world space)
///	hold shift		- enable fast movement mode
///	right mouse  	- enable free look
///	mouse			- free look / rotation
///     
/// </summary>
public class FreeCamOnlyOrient : MonoBehaviour
{

    /// <summary>
    /// Normal speed of camera movement.
    /// </summary>
    public float movementSpeed = 10f;

    /// <summary>
    /// Speed of camera movement when shift is held down,
    /// </summary>
    public float fastMovementSpeed = 100f;

    /// <summary>
    /// Sensitivity for free look.
    /// </summary>
    public float freeLookSensitivity = 3f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel.
    /// </summary>
    public float zoomSensitivity = 10f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel (fast mode).
    /// </summary>
    public float fastZoomSensitivity = 50f;

    /// <summary>
    /// Set to true when free looking (on right mouse button).
    /// </summary>
    private bool looking = false;

    private bool shiftPressed = false;

    private void Start()
    {

    }

    //private void SetHeadMovementParameter(string parameterName, float parameterValue)
    //{
    //    RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
    //}

    void Update()
    {
        var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var movementSpeed = fastMode ? this.fastMovementSpeed : this.movementSpeed;

        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.Q))
        //{
        //    transform.position = transform.position + (transform.up * movementSpeed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.position = transform.position + (-transform.up * movementSpeed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        //{
        //    transform.position = transform.position + (UnityEngine.Vector3.up * movementSpeed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
        //{
        //    transform.position = transform.position + (-UnityEngine.Vector3.up * movementSpeed * Time.deltaTime);
        //}

        if (looking)
        {
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
            transform.localEulerAngles = new UnityEngine.Vector3(newRotationY, newRotationX, 0f);
        }

        //float axis = Input.GetAxis("Mouse ScrollWheel");
        //if (axis != 0)
        //{
        //    var zoomSensitivity = fastMode ? this.fastZoomSensitivity : this.zoomSensitivity;
        //    transform.position = transform.position + transform.forward * axis * zoomSensitivity;
        //}

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            shiftPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            shiftPressed = false;
        }

        if (shiftPressed == true && Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartLooking();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopLooking();
        }

    }

    void OnDisable()
    {
        StopLooking();
    }

    /// <summary>
    /// Enable free looking.
    /// </summary>
    public void StartLooking()
    {
        looking = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //SetHeadMovementParameter(parameterName, 0.5f);
    }

    /// <summary>
    /// Disable free looking.
    /// </summary>
    public void StopLooking()
    {
        looking = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //SetHeadMovementParameter(parameterName, 0f);
    }


}
