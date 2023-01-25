using Microsoft.Maps.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapPin))]
public class PlaceActualPin : MonoBehaviour
{
    [SerializeField]
    private MapPin _actualPin;

    private MapPin _buttonPin;

    private MapPinLayer _mapPinLayer;

    private void Start()
    {
        GameObject map = GameObject.Find("Map");
        _mapPinLayer = map.GetComponent<MapPinLayer>();
        _buttonPin = GetComponent<MapPin>();
    }

    //Button click
    public void ReplacePin()
    {
        if (_mapPinLayer != null)
        {
            var newPin = Instantiate(_actualPin);
            newPin.Location = _buttonPin.Location;
            _mapPinLayer.MapPins.Add(newPin);
            Destroy(this.gameObject);
        }
        else
            Debug.LogError("Error: No Map/MapPinLayer!");
    }

}
