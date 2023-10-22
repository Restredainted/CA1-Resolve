using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//Script generated using https://forum.unity.com/threads/set-limits-for-the-position-of-the-cinemachine-camera-that-follows-the-player.1070030/

public class VCamBounds : CinemachineExtension
{
   
    [Tooltip("Lock the camera's Y position to this value")]
    public float minYPosition, maxYPosition;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {

        if (stage == CinemachineCore.Stage.Finalize)
        {

            var pos = state.RawPosition;
            pos.y = Mathf.Clamp(pos.y, minYPosition, maxYPosition);
            state.RawPosition = pos;
            
        }
    }

    
}
