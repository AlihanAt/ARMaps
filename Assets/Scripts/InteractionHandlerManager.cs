using Microsoft.Maps.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandlerManager : MonoBehaviour
{
    [SerializeField] private MixedRealityMapInteractionHandler _editorHandler;
    [SerializeField] private MapTouchInteractionHandler _touchHandler;

    private void Start()
    {
        EnableInteractionHandler();
    }

    public void EnableInteractionHandler()
    {
#if UNITY_EDITOR
        _editorHandler.enabled = true;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        _touchHandler.enabled = true;
#endif
    }


    public void DisableInteractionHandler()
    {
#if UNITY_EDITOR
        _editorHandler.enabled = false;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        _touchHandler.enabled = false;
#endif
    }

}
