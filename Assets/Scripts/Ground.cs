using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Ground : MonoBehaviour
{
    private BoxCollider boxCollider;

    public float xMax => boxCollider.size.x * transform.localScale.x / 2;
    public float zMax => boxCollider.size.z * transform.localScale.z / 2;
    public float Area => xMax * zMax * 4;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void ChangeSize(int xScale, int zScale)
    {
        transform.localScale = new Vector3(xScale, 1, zScale);
    }
}
