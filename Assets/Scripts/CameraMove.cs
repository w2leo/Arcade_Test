using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 playerOffset = new Vector3(0, 10f, -5f);
    [SerializeField] private Canvas canvas;


    private Quaternion rotationOffset;
    bool checkCamera = false;
    public Player Player { get; set; }

    private void Start()
    {
        rotationOffset = transform.rotation;

        Debug.Log($"width = {Camera.main.pixelWidth}, height = {Camera.main.pixelHeight}");
        Debug.Log($"Scl.width = {Camera.main.scaledPixelWidth}, Scl.height = {Camera.main.scaledPixelHeight}");

    }

    private void LateUpdate()
    {
        Vector3 toLerp = player.transform.position + playerOffset;
        toLerp.y = playerOffset.y + player.GetComponent<Rigidbody>().velocity.magnitude;
        Vector3 newPosition = Vector3.Lerp(transform.position, toLerp, moveSpeed * Time.deltaTime);
        //newPosition.y = playerOffset.y + player.GetComponent<Rigidbody>().velocity.magnitude;
        transform.position = newPosition;

        if (!checkCamera)
            TestCameraSize();


    }

    private void Update()
    {
        //Debug.Log($"mouse position = {Input.mousePosition}");
    }


    public void TestCameraSize()
    {
        checkCamera = true;
        Debug.Log($"T|F = {true | false}, T&F = {true & false}");
        Vector3 leftDown;
        Vector3 rightUp;
        RaycastHit hit;
        Ray ray;
        Ground ground;
        bool initCheck = true;
        //ray = Camera.main.ScreenPointToRay(new Vector3(0, 0, 0));
        //Physics.Raycast(ray, out hit);

        //if (hit.collider != null && hit.collider.TryGetComponent<Ground>(out ground))
        //{
        //    leftDown = hit.point;
        //    leftDown.y = 0f;
        //}


        //ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
        //Physics.Raycast(ray, out hit);

        //if (hit.collider != null && hit.collider.TryGetComponent<Ground>(out ground))
        //{
        //    rightUp= hit.point;
        //    rightUp.y = 0f;
        //}

        initCheck &= TryRaycastToGround(out ray, out hit, new Vector2(0, 0), out ground, out leftDown);
        initCheck &= TryRaycastToGround(out ray, out hit, new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight), out ground, out rightUp);

        if (initCheck)
        {
            float screenViewArea = Math.Abs(leftDown.x - rightUp.x) * Math.Abs(leftDown.z - rightUp.z);

            Debug.Log($"roundArea = {ground.Area}, screenViewArea = {screenViewArea}");
            Debug.Log($"Count screens in ground = {ground.Area / screenViewArea}");
        }
        else
        {
            throw new Exception("Wrong gorund / camera view setup");
        }

    }

    private bool TryRaycastToGround(out Ray ray, out RaycastHit hit, Vector2 rayScreenStart, out Ground ground, out Vector3 hitPosition)
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(rayScreenStart.x, rayScreenStart.y, 0));
        Physics.Raycast(ray, out hit);

        if (hit.collider != null && hit.collider.TryGetComponent<Ground>(out ground))
        {
            hitPosition = hit.point;
            hitPosition.y = 0f;
            return true;
        }

        ground = null;
        hitPosition = Vector3.zero;
        return false;
    }




}
