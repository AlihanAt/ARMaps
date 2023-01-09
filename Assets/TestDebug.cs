using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebug : MonoBehaviour
{
    //wird aktuell auf resizeMapButton im NearSettingsMenu genutzt

    public void DebugOnHandPressTouched()
    {
        Debug.Log("Event DebugOnHandPressTouched");
    }
    public void DebugOnHandPressUntouched()
    {
        Debug.Log("Event DebugOnHandPressUntouched");
    }
    public void DebugOnHandPressTriggered()
    {
        Debug.Log("Event DebugOnHandPressTriggered");
    }
    public void DebugOnHandPressCompleted()
    {
        Debug.Log("Event DebugOnHandPressCompleted");
    }

    public void DebugButtonConfigHelperOnClick()
    {
        Debug.Log("Event ButtonConfigHelperOnClick");
    }
}
