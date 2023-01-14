using Microsoft.Maps.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPinTest : MonoBehaviour
{
    [SerializeField]
    private MapPinLayer _mapPinLayer = null;

    [SerializeField]
    private MapPin _mapPinPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_mapPinLayer != null);
        Debug.Assert(_mapPinPrefab != null);
        AddPinToLocation();
    }

    private void AddPinToLocation()
    {
        _mapPinPrefab.gameObject.SetActive(false);
        var mapPin = Instantiate(_mapPinPrefab);
        mapPin.Location = new Microsoft.Geospatial.LatLon(52.516284, 13.377559);
        _mapPinLayer.MapPins.Add(mapPin);

        mapPin = Instantiate(_mapPinPrefab);
        mapPin.Location = new Microsoft.Geospatial.LatLon(52.516285, 13.377558);
        _mapPinLayer.MapPins.Add(mapPin);

        mapPin = Instantiate(_mapPinPrefab);
        mapPin.Location = new Microsoft.Geospatial.LatLon(52.516286, 13.377559);
        _mapPinLayer.MapPins.Add(mapPin);

        mapPin = Instantiate(_mapPinPrefab);
        mapPin.Location = new Microsoft.Geospatial.LatLon(52.516283, 13.377560);
        _mapPinLayer.MapPins.Add(mapPin);

        mapPin = Instantiate(_mapPinPrefab);
        mapPin.Location = new Microsoft.Geospatial.LatLon(52.516281, 13.377561);
        _mapPinLayer.MapPins.Add(mapPin);

        Debug.Log("MapPin created.");
    }
}
