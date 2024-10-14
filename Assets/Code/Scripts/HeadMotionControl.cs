//------------------------------------------------------------------------------
// HeadphoneMotion - Unity plugin that exposes the CMHeadphoneMotionManager API
// GitHub: https://github.com/anastasiadevana/HeadphoneMotion
//------------------------------------------------------------------------------
//
// MIT License
//
// Copyright (c) 2020 Anastasia Devana
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//------------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using TMPro.Examples;
using TMPro;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

namespace HearXR
{
    /// <summary>
    /// Example of using HeadphoneMotion.
    /// </summary>
    public class HeadMotionControl : MonoBehaviour
    {
        #region Editor Fields
        [SerializeField] private Transform _rotateTarget = default;
        [SerializeField] private Transform _rotate2ndTarget = default;
        //[SerializeField] private Text _motionAvailabilityText = default;
        //[SerializeField] private Text _headphoneConnectionStatusText = default;
        //[SerializeField] private Button _toggleTrackingButton = default;
        [SerializeField] private Button _calibrateStartingRotationButton = default;
        [SerializeField] private Button _resetCalibrationButton = default;

        [SerializeField] private TextMeshProUGUI headTextOnHeadmotionError;
        [SerializeField] private TextMeshProUGUI bodyTextOnHeadmotionError;

        [SerializeField] private TextMeshProUGUI headphonesConnectedStatus;
        [SerializeField] private TextMeshProUGUI headphonesNotConnectedStatus;
        [SerializeField] private TextMeshProUGUI textPromptRestart;
        #endregion

        #region Private Fields
        private bool _motionAvailable;
        private bool _tracking;
        private bool _headphonesConnected;
        //private TMPro.TextMeshProUGUI _trackingButtonText;
        private Quaternion _lastRotation = Quaternion.identity;
        private Quaternion _calibratedOffset = Quaternion.identity;
        #endregion

        #region Test Bools
        //[SerializeField] private bool setHeadphoneConnectionToON;
        //[SerializeField] private bool setTrackingToON;
        //[SerializeField] private bool setMotionAvailableToON;
        #endregion

        #region Init
        private void Start()
        {
            // Add event listeners to buttons and hide buttons as needed.
            //_toggleTrackingButton.onClick.AddListener(ToggleTracking);
            //_trackingButtonText = _toggleTrackingButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();

            _calibrateStartingRotationButton.onClick.AddListener(CalibrateStartingRotation);
            _resetCalibrationButton.onClick.AddListener(ResetCalibration);
            UpdateRotationOffsetButtons();
            
            // Init HeadphoneMotion. Always call this first.
            HeadphoneMotion.Init();

            // Check if headphone motion is available on this device.
            _motionAvailable = HeadphoneMotion.IsHeadphoneMotionAvailable();

            //_motionAvailabilityText.text =
                //(_motionAvailable) ? "Headphone motion is available" : "Headphone motion is not available";
            HeadMotionError(_motionAvailable);

            if (_motionAvailable)
            {
                // Set headphones connected text to false to start with.
                HandleHeadphoneConnectionChange(false);
                
                // Subscribe to events before starting tracking, or will miss the initial headphones connected callback.
                // Subscribe to the headphones connected/disconnected event.
                HeadphoneMotion.OnHeadphoneConnectionChanged += HandleHeadphoneConnectionChange;

                // Subscribe to the rotation callback.
                HeadphoneMotion.OnHeadRotationQuaternion += HandleHeadRotationQuaternion;
                //HeadphoneMotion.OnHeadRotationQuaternion += HandleHeadRotationQuaternionInvert;

                // Start tracking headphone motion.
                HeadphoneMotion.StartTracking();
                _tracking = true;
            }

            //UpdateTrackingButton();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Headphone connection status was changed (callback for OnHeadphoneConnectionChanged()).
        /// </summary>
        /// <param name="connected">TRUE if connected, FALSE otherwise.</param>
        private void HandleHeadphoneConnectionChange(bool connected)
        {
            _headphonesConnected = connected;
            headphonesConnectedStatus.DOFade(_headphonesConnected ? 1.0f : 0.0f, .2f).SetUpdate(true);
            headphonesNotConnectedStatus.DOFade(_headphonesConnected ? 0.0f : 1.0f, .2f).SetUpdate(true);
            textPromptRestart.DOFade(_headphonesConnected ? 0f : 1f, .2f).SetUpdate(true);


            //_headphoneConnectionStatusText.text =
            //(_headphonesConnected) ? "Headphones are connected" : "Headphones are not connected";

            UpdateRotationOffsetButtons();
        }

        /// <summary>
        /// Receive headphone as quaternion (callback for OnHeadRotationQuaternion()).
        /// </summary>
        /// <param name="rotation">Headphone rotation</param>
        //private void HandleHeadRotationQuaternion(Quaternion rotation)
        //{
        //    // Match the target object's rotation to the headphone rotation.
        //    if (_calibratedOffset == Quaternion.identity)
        //    {
        //        _rotateTarget.rotation = rotation;
        //        _rotate2ndTarget.rotation = rotation;
        //    }
        //    else
        //    {
        //        _rotateTarget.rotation = rotation * Quaternion.Inverse(_calibratedOffset);
        //        _rotate2ndTarget.rotation = rotation * Quaternion.Inverse(_calibratedOffset);
        //    }

        //    _lastRotation = rotation;
        //}
        private void HandleHeadRotationQuaternion(Quaternion rotation)
        {
            // Rotate _rotateTarget normally
            if (_calibratedOffset == Quaternion.identity)
            {
                _rotateTarget.rotation = rotation;
            }
            else
            {
                _rotateTarget.rotation = rotation * Quaternion.Inverse(_calibratedOffset);
            }

            // Invert rotation for _rotate2ndTarget on both Y and X axes
            Vector3 invertedEulerAngles = new Vector3(-rotation.eulerAngles.x, -rotation.eulerAngles.y, rotation.eulerAngles.z);
            Quaternion invertedRotation = Quaternion.Euler(invertedEulerAngles);

            if (_calibratedOffset == Quaternion.identity)
            {
                _rotate2ndTarget.rotation = invertedRotation;
            }
            else
            {
                _rotate2ndTarget.rotation = invertedRotation * Quaternion.Inverse(_calibratedOffset);
            }

            _lastRotation = rotation;
        }


        #endregion

        #region Private Methods
        private void ToggleTracking()
        {
            _tracking = !_tracking;

            if (_motionAvailable && _tracking)
            {
                HeadphoneMotion.StartTracking();
            }
            else
            {
                HeadphoneMotion.StopTracking();
            }

            //UpdateTrackingButton();
        }

        private void CalibrateStartingRotation()
        {
            Debug.Log("CalibrateStartingRotation have been called");
            _calibratedOffset = _lastRotation;
            UpdateRotationOffsetButtons();
        }

        private void ResetCalibration()
        {
            Debug.Log("reset calibration pressed");
            _calibratedOffset = Quaternion.identity;
            UpdateRotationOffsetButtons();
        }

        //private void UpdateTrackingButton()
        //{
        //    if (!_motionAvailable)
        //    {
        //        //_toggleTrackingButton.gameObject.SetActive(false);
        //        return;
        //    }

        //    _trackingButtonText.text = (_tracking) ? "Disable tracking" : "Enable tracking";
        //}

        private void UpdateRotationOffsetButtons()
        {
            //_calibrateStartingRotationButton.gameObject.SetActive(_tracking && _headphonesConnected);
            //_resetCalibrationButton.gameObject.SetActive(_tracking && _headphonesConnected && _calibratedOffset != Quaternion.identity);
        }

        private void HeadMotionError(bool _bool)
        {
            if(_bool) { Debug.Log("HeadMotionAvailable is true"); }
            else { Debug.Log("HeadMotionAvailable is false"); }
            headTextOnHeadmotionError.DOFade(_bool ? 0f : 1f, .2f).SetUpdate(true);
            bodyTextOnHeadmotionError.DOFade(_bool ? 0f : 1f, .2f).SetUpdate(true);
            //textPromptRestart.DOFade(_bool ? 0f : 1f, .2f).SetUpdate(true);
        }

        #endregion
    }
}
