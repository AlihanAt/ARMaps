using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TR = UnityEngine.XR.ARSubsystems.TrackableType;

public class TouchToPlace : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private ARPoseDriver _poseDriver;
    [SerializeField] private TR _trackableType = TR.PlaneEstimated;
    [SerializeField] private GameObject _cube;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private GameObject _go;


    void Update()
    {
        if (Input.touchCount == 0)
            return;

        var pos = Input.GetTouch(0).position;

        if (_raycastManager.Raycast(pos, _hits, _trackableType))
        {
            Debug.Log("Raycast hit");
            var hitPose = _hits[0].pose;

            if (_hits[0].trackable is ARPlane plane)
            {
                if(_go == null)
                {
                    Debug.Log("Object Instantiate");
                    _go = Instantiate(_cube, hitPose.position, Quaternion.identity);
                }
                else
                {
                    Debug.Log("Object Moved");
                    _go.transform.position = hitPose.position;
                }

                Debug.Log("Object Pos: " + _go.transform.position + ", User Pos: " + _poseDriver.transform.position);
            }
        }
    }
}
