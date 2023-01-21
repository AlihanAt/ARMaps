using Microsoft.Maps.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    [SerializeField] private MapPinLayer _mapPinLayer;
    [SerializeField] private GPSTracking _gpsTracking;
    [SerializeField] private GameObject _pinObject;
    [SerializeField] private List<MapPin> _pinInRangeList = new List<MapPin>();
    private List<MapPin> _placedPinList = new List<MapPin>();
    private Dictionary<MapPin, GameObject> _pinInRangeMap = new Dictionary<MapPin, GameObject>();

    private readonly int _loadingRange = 20;

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

    private void ShowPins()
    {
        foreach(MapPin pin in _pinInRangeList)
        {
            if (!_placedPinList.Contains(pin))
            {
                var placedPin = Instantiate(_pinObject);
                _placedPinList.Add(pin);
            }
        }
    }

    private void HidePins()
    {
        foreach (MapPin pin in _mapPinLayer.MapPins)
        {
            //if (!_placedPinList.Contains(pin))

        }

    }

    private void PinRangeCheck()
    {
        foreach (MapPin pin in _mapPinLayer.MapPins)
        {
            if (_gpsTracking.CalculateDistanceTo(pin.Location) < _loadingRange)
            {
                if (!_pinInRangeList.Contains(pin))
                {
                    AddPin(pin);
                }
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

    private void AddPin(MapPin pin)
    {
        _pinInRangeList.Add(pin);
        var tmpPin = Instantiate(_pinObject);
        _pinInRangeMap.Add(pin, tmpPin);
        Debug.Log("Pin added");
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
