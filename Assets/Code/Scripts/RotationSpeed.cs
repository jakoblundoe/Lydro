using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSpeed : MonoBehaviour
{
    [Tooltip("Processed speed based on topSpeed and lerpDuration")]
    [SerializeField] public float rotationSpeed;

    [Tooltip("The duration for winding currentRotationSpeed up/down")]
    [SerializeField] private float lerpDuration = 0.3f;

    [Tooltip("Determines the value rawRotationSpeed needs to hit for currentRotationSpeed to hit 1")]
    [SerializeField] private float topSpeed = 10f;

    [Tooltip("The raw angular speed of the object")]
    [SerializeField] private float rawRotationSpeed;


    private Quaternion lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        lastRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation != lastRotation)
        {
            rawRotationSpeed = Quaternion.Angle(lastRotation, transform.rotation);
        }
        else
        {
            rawRotationSpeed = 0;
        }

        rotationSpeed = Mathf.Clamp(Mathf.Lerp(rotationSpeed, rawRotationSpeed / topSpeed, Time.deltaTime / lerpDuration), 0, 1);

        if (rawRotationSpeed == 0 && rotationSpeed < 0.01f)
        {
            rotationSpeed = 0;
        }

        lastRotation = transform.rotation;
    }
}