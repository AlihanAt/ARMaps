using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebug : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

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
