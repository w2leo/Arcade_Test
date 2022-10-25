using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Joystick joysticInput;
    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = GetDirection();
        transform.position += direction * moveSpeed * Time.fixedDeltaTime;
        //playerRb.AddForce(direction * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
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
        return Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");
    }

    private bool TryJoysticInput(out Vector3 direction)
    {
        direction = joysticInput.Direction3d;
        if (direction != Vector3.zero)
            return true;
        return false;
    }
}
