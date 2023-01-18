using Microsoft.Maps.Unity;
using Microsoft.Maps.Unity.Search;
using UnityEngine;
using Microsoft.Geospatial;

[RequireComponent(typeof(MapRenderer))]
public class MapCoordinate : MonoBehaviour
{
    [SerializeField] private GameObject _placePinButton;

    private MapRenderer _mapRenderer;
    private MapPinTest _mapPinTest;

    public void Awake()
    {
        _mapRenderer = GetComponent<MapRenderer>();
        _mapPinTest = GetComponent<MapPinTest>();
        Debug.Assert(_mapRenderer != null);
    }

    //ein event in interactionhandler und touchinteractionhandler
    //bei MapInteractionController Component beim Event reinziehen
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

        _mapPinTest.AddPinToLocation(latLonAlt.LatLon);


        Debug.Log(latLonAlt.LatLon);
    }
}
