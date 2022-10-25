using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 playerOffset = new Vector3(0, 10f, -5f);
    private Quaternion rotationOffset;

    public Player Player { get; set; }

    private void Start()
    {
        rotationOffset = transform.rotation;
    }

    private void LateUpdate()
    {
        Vector3 toLerp = player.transform.position + playerOffset;
        toLerp.y = playerOffset.y + player.GetComponent<Rigidbody>().velocity.magnitude;
        Vector3 newPosition = Vector3.Lerp(transform.position, toLerp, moveSpeed * Time.deltaTime);
        //newPosition.y = playerOffset.y + player.GetComponent<Rigidbody>().velocity.magnitude;
        transform.position = newPosition;
    }


}
