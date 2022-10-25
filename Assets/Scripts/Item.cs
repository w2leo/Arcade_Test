using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ParticleSystem particleExplosion;

    public float Size => GetComponent<MeshFilter>().sharedMesh.bounds.size.x; // or Z ???? refactor Later

    public void FirstInitialize(Vector3 initPosition, ParticleSystem particleExplosion)
    {
        transform.position = initPosition;
        this.particleExplosion = particleExplosion;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            particleExplosion.transform.position = transform.position;
            particleExplosion.Play();
            Destroy(gameObject);
        }
    }

 
}
