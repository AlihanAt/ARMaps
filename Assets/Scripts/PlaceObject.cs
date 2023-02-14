using Microsoft.Geospatial;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject _pinObject;
    [SerializeField] private GameObject _arSessionOrigin;
    [SerializeField] private GameObject _pinParent;
    [SerializeField] private GameObject _menu;

    private PinManager _pinManager;
    private Camera _mainCamera;
    private GameObject _placedObject;

    private bool _placeNow = false;

    void Start()
    {
        _mainCamera = _arSessionOrigin.GetComponentInChildren<Camera>();
        _pinManager = GetComponent<PinManager>();
    }

    int count = 0;
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && _placeNow)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                ConfirmPlacement();
        }
    }

    public void InstantiatePinObject()
    {
        _placedObject = Instantiate(_pinObject, _mainCamera.transform);
        _placedObject.transform.localPosition = new Vector3(0, 0, 1);
        _placeNow = true;
        _menu.SetActive(false);
    }

    private void ConfirmPlacement()
    {
        _pinManager.PlaceNewPin(_placedObject);
        _placedObject = null;
        _placeNow = false;
        _menu.SetActive(true);
    }
}
