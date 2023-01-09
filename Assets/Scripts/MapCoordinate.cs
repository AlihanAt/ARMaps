using Microsoft.Maps.Unity;
using Microsoft.Maps.Unity.Search;
using UnityEngine;
using Microsoft.Geospatial;

[RequireComponent(typeof(MapRenderer))]
public class MapCoordinate : MonoBehaviour
{
    private MapRenderer _mapRenderer;

    public void Awake()
    {
        _mapRenderer = GetComponent<MapRenderer>();
        Debug.Assert(_mapRenderer != null);
    }

    //wo wird das nochmal genutzt? vergessen, rip
    public async void OnTapAndHold(LatLonAlt latLonAlt)
    {
        if (ReferenceEquals(MapSession.Current, null) || string.IsNullOrEmpty(MapSession.Current.DeveloperKey))
        {
            Debug.LogError(
                "Provide a Bing Maps key to use the map services. " +
                "This key can be set on a MapSession component.");
            return;
        }

        var finderResult = await MapLocationFinder.FindLocationsAt(latLonAlt.LatLon);

        string formattedAddressString = null;
        if (finderResult.Locations.Count > 0)
        {
            formattedAddressString = finderResult.Locations[0].Address.FormattedAddress;
        }

        Debug.Log(latLonAlt.LatLon);
    }
}
