using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Item : MonoBehaviour
{
    private ParticleSystem particleExplosion;
    private MeshFilter meshFilter;
    public float Size => meshFilter.sharedMesh.bounds.size.x;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            particleExplosion.transform.position = transform.position;
            particleExplosion.Play();
            Destroy(gameObject);

            // method to Spawner (colect item)
        }
    }

    public void FirstInitialize(Vector3 initPosition, ParticleSystem particleExplosion)
    {
        transform.position = initPosition;
        this.particleExplosion = particleExplosion;
    }
}
