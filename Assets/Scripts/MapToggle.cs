using Microsoft.Maps.Unity;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{
    [SerializeField] private MapRenderer _mapRenderer;
    [SerializeField] private Interactable _placeMapButton;
    [SerializeField] private Interactable _resizeMapButton;

    // Start is called before the first frame update
    void Start()
    {
        this._mapRenderer = GetComponent<MapRenderer>();        
    }

    public void ToggleMap()
    {
        _mapRenderer.enabled = !_mapRenderer.isActiveAndEnabled;
        ToggleButtons();
    }

    private void ToggleButtons()
    {
        if (_mapRenderer.enabled)
        {
            _placeMapButton.enabled = true;
            _resizeMapButton.enabled = true;
        }
        else
        {
            _placeMapButton.enabled = false;
            _resizeMapButton.enabled = false;
        }
    }
}
