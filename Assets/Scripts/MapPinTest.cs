using Microsoft.Geospatial;
using Microsoft.Maps.Unity;
using UnityEngine;

public class MapPinTest : MonoBehaviour
{
    [SerializeField]
    private MapPinLayer _mapPinLayer = null;

    [SerializeField]
    private MapPin _mapPinButtonPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_mapPinLayer != null);
        Debug.Assert(_mapPinButtonPrefab != null);
    }

    public void AddPinToLocation(LatLon latLon)
    {
        //_mapPinButtonPrefab.gameObject.SetActive(false);
        var mapPin = Instantiate(_mapPinButtonPrefab);
        _mapPinButtonPrefab.gameObject.SetActive(true);
        _mapPinButtonPrefab.Location = latLon;
        _mapPinLayer.MapPins.Add(mapPin);
        //_mapPinLayer.MapPins.Clear();

        //mapPin = Instantiate(_mapPinPrefab);
        //mapPin.Location = new Microsoft.Geospatial.LatLon(52.516285, 13.377558);
        //_mapPinLayer.MapPins.Add(mapPin);

        //mapPin = Instantiate(_mapPinPrefab);
        //mapPin.Location = new Microsoft.Geospatial.LatLon(52.516286, 13.377559);
        //_mapPinLayer.MapPins.Add(mapPin);

        //mapPin = Instantiate(_mapPinPrefab);
        //mapPin.Location = new Microsoft.Geospatial.LatLon(52.516283, 13.377560);
        //_mapPinLayer.MapPins.Add(mapPin);

        //mapPin = Instantiate(_mapPinPrefab);
        //mapPin.Location = new Microsoft.Geospatial.LatLon(52.516281, 13.377561);
        //_mapPinLayer.MapPins.Add(mapPin);

        Debug.Log("MapPin created.");
    }
}
