using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraViewState 
{
    private static Camera mainCamera;

    public static float ViewArea => GetCameraViewArea();

    static CameraViewState()
    {
        mainCamera = Camera.main;
    }

    private static float GetCameraViewArea()
    {
        bool initCheck = true;
        initCheck &= TryRaycastToGround(new Vector2(0, 0), out Vector3 leftDown);
        initCheck &= TryRaycastToGround(new Vector2(mainCamera.pixelWidth, mainCamera.pixelHeight), out Vector3 rightUp);
        if (initCheck)
        {
            return Math.Abs(leftDown.x - rightUp.x) * Math.Abs(leftDown.z - rightUp.z);
        }
        else
        {
            throw new Exception("Wrong gorund / camera view setup");
        }
    }

    private static bool TryRaycastToGround(Vector2 rayScreenStart, out Vector3 hitPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(rayScreenStart.x, rayScreenStart.y, 0));
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider != null && hit.collider.TryGetComponent<Ground>(out _))
        {
            hitPosition = hit.point;
            hitPosition.y = 0f;
            return true;
        }
        hitPosition = Vector3.zero;
        return false;
    }
}
