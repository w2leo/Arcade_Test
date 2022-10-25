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
        //Vector3 direction = Vector3.forward * joysticInput.Vertical + Vector3.right * joysticInput.Horizontal;
        Vector3 direction = GetDirection();
        playerRb.AddForce(direction * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    private Vector3 GetDirection()
    {
#if UNITY_ANDROID
        return Vector3.forward * joysticInput.Vertical + Vector3.right * joysticInput.Horizontal;
#endif

#if UNITY_STANDALONE
        return Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");
#endif
    }

}
