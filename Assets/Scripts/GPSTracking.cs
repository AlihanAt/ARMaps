using Microsoft.Geospatial;
using Microsoft.Maps.Unity;
using System.Collections;
using TMPro;
using UnityEngine;

public class GPSTracking : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _debugtext;
    [SerializeField] private MapRenderer _mapRenderer;
    [SerializeField] private MapPin _playerPin;

    private LatLon _currentPos;
    private float _gpsUpdateTimer = 0.3f;

    private bool _focusPlayer = true;
    private bool _focussing;

    private LatLon TEST_GPS_VALUES;

    private double _tmpLat;
    private double _tmpLon;

    void Awake()
    {
        _tmpLat = TEST_GPS_VALUES.LatitudeInDegrees;
        _tmpLon = TEST_GPS_VALUES.LongitudeInDegrees;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("w"))
        {
            _tmpLat += 0.00005;
        }
        if (Input.GetKeyDown("a"))
        {
            _tmpLon -= 0.00005;
        }
        if (Input.GetKeyDown("s"))
        {
            _tmpLat -= 0.00005;
        }
        if (Input.GetKeyDown("d"))
        {
            _tmpLon += 0.00005;
        }
#endif
    }

    IEnumerator Start()
    {
#if UNITY_EDITOR
        SetMyTestPosition();
        StartCoroutine(UpdateGPS());
        StartCoroutine(FocusMapOnPlayerPin());
#endif

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
            _debugtext.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            Debug.LogWarning(_currentPos);
            StartCoroutine(UpdateGPS());
            StartCoroutine(FocusMapOnPlayerPin());
        }
    }

    IEnumerator UpdateGPS()
    {
        while (true)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            Debug.Log("UNITY_ANDROID && !UNITY_EDITOR");
            _currentPos = new LatLon(Input.location.lastData.latitude, Input.location.lastData.longitude);
#endif
#if UNITY_EDITOR
            _currentPos = new LatLon(_tmpLat, _tmpLon);
#endif
            _playerPin.Location = _currentPos;
            yield return new WaitForSeconds(_gpsUpdateTimer);
        }
    }

    private IEnumerator FocusMapOnPlayerPin()
    {
        _focussing = true;
        while (_focusPlayer)
        {
            _mapRenderer.SetMapScene(new MapSceneOfLocationAndZoomLevel(_currentPos, _mapRenderer.ZoomLevel));
            yield return new WaitForSeconds(1);
        }
        _focussing = false;
    }

    public void FocusPlayer(bool focus)
    {
        _focusPlayer = focus;
        
        if(!_focussing && focus)
        {
            StartCoroutine(FocusMapOnPlayerPin());
        }
    }

    private void SetMyTestPosition()
    {
        _currentPos = TEST_GPS_VALUES;
        _playerPin.Location = _currentPos;
    }

    public float CalculateDistanceTo(LatLon target)
    {
        //https://answers.unity.com/questions/1221259/how-to-get-distance-from-2-locations-with-unity-lo.html

        int R = 6371;
        var lat_rad_1 = Mathf.Deg2Rad * (float)_currentPos.LatitudeInDegrees;
        var lat_rad_2 = Mathf.Deg2Rad * (float)target.LatitudeInDegrees;
        var d_lat_rad = Mathf.Deg2Rad * (float)(target.LatitudeInDegrees - _currentPos.LatitudeInDegrees);
        var d_long_rad = Mathf.Deg2Rad * (float)(target.LongitudeInDegrees - _currentPos.LongitudeInDegrees);
        var a = Mathf.Pow(Mathf.Sin(d_lat_rad / 2), 2) + (Mathf.Pow(Mathf.Sin(d_long_rad / 2), 2) * Mathf.Cos(lat_rad_1) * Mathf.Cos(lat_rad_2));
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        var total_dist = R * c * 1000; // convert to meters
        return total_dist;
    }

    public double CalculateBearingTo(LatLon target)
    {
        //https://stackoverflow.com/questions/2042599/direction-between-2-latitude-longitude-points-in-c-sharp

        float dLon = Mathf.Deg2Rad * (float)(target.LongitudeInDegrees - _currentPos.LongitudeInDegrees);
        float dPhi = Mathf.Log(Mathf.Tan((Mathf.Deg2Rad * (float)target.LatitudeInDegrees) / (2 + Mathf.PI / 4)) / Mathf.Tan((Mathf.Deg2Rad * (float)_currentPos.LatitudeInDegrees) / (2 + Mathf.PI / 4)));
        if (Mathf.Abs(dLon) > Mathf.PI)
            dLon = dLon > 0 ? -(2 * Mathf.PI - dLon) : (2 * Mathf.PI + dLon);

        var bearing = Mathf.Rad2Deg * Mathf.Atan2(dLon, dPhi);
        if (bearing < 0)
            bearing += 360;

        return bearing;
    }

    void OnDisable()
    {
        Input.location.Stop();
    }
}
