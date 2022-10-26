using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float cameraHeight = 10f;
    [SerializeField] private float zCameraOffset = -5f;

    private float viewArea;
    private Vector3 cameraOffset;

    public float ViewArea => viewArea;

    private void Start()
    {
        cameraOffset = new Vector3(0f, cameraHeight, zCameraOffset);
        viewArea = GetCameraViewArea();
    }

    private void LateUpdate()
    {
        Vector3 newCameraPosition = player.transform.position + cameraOffset;
        newCameraPosition.y = cameraOffset.y;
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, moveSpeed * Time.deltaTime);
    }

    private float GetCameraViewArea()
    {
        bool initCheck = true;
        initCheck &= TryRaycastToGround(new Vector2(0, 0), out Vector3 leftDown);
        initCheck &= TryRaycastToGround(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight), out Vector3 rightUp);
        if (initCheck)
        {
            return Math.Abs(leftDown.x - rightUp.x) * Math.Abs(leftDown.z - rightUp.z);
        }
        else
        {
            throw new Exception("Wrong gorund / camera view setup");
        }
    }

    private bool TryRaycastToGround(Vector2 rayScreenStart, out Vector3 hitPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(rayScreenStart.x, rayScreenStart.y, 0));
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
