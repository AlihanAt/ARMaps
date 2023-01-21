using Microsoft.Geospatial;
using Microsoft.Maps.Unity;
using System.Collections;
using TMPro;
using UnityEngine;

public class GPSTracking : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _debugtext;
    
    private string _currentPosString;
    private LatLon _currentPos;

    private MapRenderer _map;
    
    [SerializeField] private MapPin _playerPin;

    private LatLon TEST_GPS_VALUES;

    void Awake()
    {
        _map = GameObject.Find("Map").GetComponent<MapRenderer>();
        Debug.Assert(_map != null);
    }

    IEnumerator Start()
    {
        _currentPos = TEST_GPS_VALUES;
        ShowOwnPosition();
        SetMyPosition();

        //Example
        //CalculateDistance(52, 53, 13, 14);

        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("NO GPS");
            yield break;
        }

        Input.location.Start(10, 10);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.LogWarning("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("Unable to determine device location");
            yield break;
        }
        else
        {
            _currentPos = new LatLon(Input.location.lastData.latitude, Input.location.lastData.longitude);
            _currentPosString = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            Debug.LogWarning(_currentPos);
            _debugtext.text = _currentPosString;
        }

        //ShowOwnPosition();
        //SetMyPosition();

        // Stops the location service if there is no need to query location updates continuously.
        //Input.location.Stop();
    }

//https://answers.unity.com/questions/1221259/how-to-get-distance-from-2-locations-with-unity-lo.html
//erst beide lat, dann long
//in meters
    public float CalculateDistance(float lat_1, float lat_2, float long_1, float long_2)
    {
        int R = 6371;
        var lat_rad_1 = Mathf.Deg2Rad * lat_1;
        var lat_rad_2 = Mathf.Deg2Rad * lat_2;
        var d_lat_rad = Mathf.Deg2Rad * (lat_2 - lat_1);
        var d_long_rad = Mathf.Deg2Rad * (long_2 - long_1);
        var a = Mathf.Pow(Mathf.Sin(d_lat_rad / 2), 2) + (Mathf.Pow(Mathf.Sin(d_long_rad / 2), 2) * Mathf.Cos(lat_rad_1) * Mathf.Cos(lat_rad_2));
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        var total_dist = R * c * 1000; // convert to meters
        Debug.Log("Calced Distance: " + total_dist);
        return total_dist;
    }

    public float CalculateDistanceTo(LatLon target)
    {
        int R = 6371;
        var lat_rad_1 = Mathf.Deg2Rad * (float) _currentPos.LatitudeInDegrees;
        var lat_rad_2 = Mathf.Deg2Rad * (float) target.LatitudeInDegrees;
        var d_lat_rad = Mathf.Deg2Rad * (float) (target.LatitudeInDegrees - _currentPos.LatitudeInDegrees);
        var d_long_rad = Mathf.Deg2Rad * (float) (target.LongitudeInDegrees - _currentPos.LongitudeInDegrees);
        var a = Mathf.Pow(Mathf.Sin(d_lat_rad / 2), 2) + (Mathf.Pow(Mathf.Sin(d_long_rad / 2), 2) * Mathf.Cos(lat_rad_1) * Mathf.Cos(lat_rad_2));
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        var total_dist = R * c * 1000; // convert to meters
        Debug.Log("Calced Distance: " + total_dist);
        return total_dist;
    }

    private void ShowOwnPosition()
    {
        var pos = TEST_GPS_VALUES;
        var mapScene = new MapSceneOfLocationAndZoomLevel(pos, _map.ZoomLevel + 1f);
        _map.SetMapScene(mapScene);
    }

    private void SetMyPosition()
    {
        _playerPin.Location = new LatLon(52.421261, 13.427373);
    }

    public LatLon GetCurrentPos()
    {
        return _currentPos;
    }
}
