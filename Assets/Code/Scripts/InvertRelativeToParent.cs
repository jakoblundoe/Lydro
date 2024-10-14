//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.Networking.Types;

//public class InvertRelativeToParent : MonoBehaviour
//{
//    private Transform parentTransform;

//    [SerializeField] private Transform _rotateTarget;

//    //private Vector3 initialLocalPosition;
//    private Quaternion initialLocalRotation;

//    private void Start()
//    {
//        // Store the parent's transform
//        parentTransform = transform.parent;

//        // Store initial local position and rotation relative to parent
//        //initialLocalPosition = transform.localPosition;
//        initialLocalRotation = Quaternion.Inverse(transform.localRotation);
//    }

//    private void Update()
//    {
//        // get your target's rotation
//        Quaternion targetRotation = _rotateTarget.transform.rotation;

//        // choose an object and multiply its axis to reverse
//        transform.rotation = targetRotation * Quaternion.Euler(0, 180f, 0);
//    }
//}


