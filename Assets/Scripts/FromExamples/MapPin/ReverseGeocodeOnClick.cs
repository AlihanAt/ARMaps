// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Geospatial;
using Microsoft.Maps.Unity;
using UnityEngine;

/// <summary>
/// Instantiates a <see cref="MapPin"/> for each location that is reverse geocoded.
/// The <see cref="MapPin"/> will display the address of the reverse geocoded location.
/// </summary>
[RequireComponent(typeof(MapRenderer))]
public class ReverseGeocodeOnClick : MonoBehaviour
{
    private MapRenderer _mapRenderer;

    [SerializeField]
    private MapPinLayer _mapPinLayer;

    [SerializeField]
    private MapPin _mapButtonPinPrefab;

    private MapPin _mapButtonPinGO;

    public void Awake()
    {
        _mapRenderer = GetComponent<MapRenderer>();
        Debug.Assert(_mapRenderer != null);
        Debug.Assert(_mapPinLayer != null);
    }

    //ein event in interactionhandler und touchinteractionhandler
    //bei MapInteractionController Component beim Event reinziehen
    public void OnTapAndHold(LatLonAlt latLonAlt)
    {
        if (ReferenceEquals(MapSession.Current, null) || string.IsNullOrEmpty(MapSession.Current.DeveloperKey))
        {
            Debug.LogError(
                "Provide a Bing Maps key to use the map services. " +
                "This key can be set on a MapSession component.");
            return;
        }

        if (_mapButtonPinPrefab != null)
        {
            if(_mapButtonPinGO == null)
            {
                _mapButtonPinGO = Instantiate(_mapButtonPinPrefab, this.gameObject.transform);
            }
            _mapButtonPinGO.Location = latLonAlt.LatLon;
        }
    }
}