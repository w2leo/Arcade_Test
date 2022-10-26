using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float cameraHeight = 10f;
    [SerializeField] private float zCameraOffset = -5f;

    private Vector3 cameraOffset => new Vector3(0f, cameraHeight, zCameraOffset);

    private Vector3 CameraPosition => player.transform.position + cameraOffset;

    public void SetCameraToPlayer()
    {
        transform.position = CameraPosition;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, CameraPosition, moveSpeed * Time.deltaTime);
    }
}
