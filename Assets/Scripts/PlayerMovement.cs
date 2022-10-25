using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private Joystick joysticInput;
    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 direction = GetDirection();
        //transform.position += direction * moveSpeed * Time.deltaTime;
        //playerRb.AddForce(direction * moveSpeed, ForceMode.Force);
        //playerRb.velocity = direction * moveSpeed;
        playerRb.velocity = Vector3.Lerp(playerRb.velocity, direction * moveSpeed, acceleration * Time.deltaTime);
    }

    private Vector3 GetDirection()
    {
        if (!TryJoysticInput(out Vector3 newDirection))
        {
            newDirection = GetKeybordDirection();
        }
        return newDirection;
    }

    private Vector3 GetKeybordDirection()
    {
        Vector3 direction = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");
        if (direction.magnitude > 1)
            direction = direction.normalized;
        return direction;
    }
    private bool TryJoysticInput(out Vector3 direction)
    {
        direction = joysticInput.Direction3d;
        if (direction != Vector3.zero)
            return true;
        return false;
    }
}
