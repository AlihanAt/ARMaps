using Microsoft.Maps.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    [SerializeField] private MapPinLayer _mapPinLayer;
    [SerializeField] private GPSTracking _gpsTracking;
    [SerializeField] private GameObject _pinParent;
    [SerializeField] private GameObject _pinObject;
    [SerializeField] private List<MapPin> _pinInRangeList = new List<MapPin>();
    private List<MapPin> _placedPinList = new List<MapPin>();
    private Dictionary<MapPin, GameObject> _pinInRangeMap = new Dictionary<MapPin, GameObject>();

    private readonly int _loadingRange = 100;

    private readonly float _resetTime = 1f;
    private float _time = 1f;

    void Update()
    {
        _time -= Time.deltaTime;
        if (_time < 0)
        {
            PinRangeCheck();
            _time = _resetTime;
        }
    }

    private void PinRangeCheck()
    {
        foreach (MapPin pin in _mapPinLayer.MapPins)
        {
            var distance = _gpsTracking.CalculateDistanceTo(pin.Location);
            if ( distance < _loadingRange)
            {
                var bearing = _gpsTracking.CalculateBearingTo(pin.Location);
                if (!_pinInRangeList.Contains(pin))
                {
                    AddPin(pin, distance, bearing);
                }
                UpdatePin(pin, distance, bearing);
            }
            else
            {
                if (_pinInRangeList.Contains(pin))
                {
                    RemovePin(pin);
                }
            }
        }
    }

    private void UpdatePin(MapPin pin, float distance, double bearing)
    {
        _pinInRangeMap.TryGetValue(pin, out GameObject pinToUpdate);
        Debug.Log("parten rotation: " + _pinParent.transform.rotation.y);
        var vector = Quaternion.Euler(0, (float) bearing - _pinParent.transform.rotation.y, 0) * Vector3.forward * distance;
        pinToUpdate.transform.position = vector + _pinParent.transform.position;
    }

    private void AddPin(MapPin pin, float distance, double bearing)
    {
        var tmpPin = Instantiate(_pinObject, _pinParent.transform);
        var vector = Quaternion.Euler(0, (float)bearing, 0) * Vector3.forward * distance;
        tmpPin.transform.position = vector;

        _pinInRangeList.Add(pin);
        _pinInRangeMap.Add(pin, tmpPin);
        Debug.Log("Pin added at " + pin.Location);
    }

    private void RemovePin(MapPin pin)
    {
        _pinInRangeList.Remove(pin);
        _pinInRangeMap.TryGetValue(pin, out GameObject placedPinObject);
        _pinInRangeMap.Remove(pin);
        Destroy(placedPinObject.gameObject);
        Debug.Log("Pin removed");
    }
}
