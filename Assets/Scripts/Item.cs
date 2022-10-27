using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Item : MonoBehaviour
{
    private ParticleSystem particleExplosion;
    private MeshFilter meshFilter;
    private GameController gameController;

    public int ItemIndex { get; private set; }

    public float Size => meshFilter.sharedMesh.bounds.size.x;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            // do smth with Player and itself
            PlayExplosion();
            gameController.CollectItem(this);
        }
    }

    public void FirstInitialize(Vector3 initPosition, ParticleSystem particleExplosion, GameController gameController, int index)
    {
        transform.position = initPosition;
        this.particleExplosion = particleExplosion;
        this.gameController = gameController;
        ItemIndex = index;
    }

    private void PlayExplosion()
    {
        particleExplosion.transform.position = transform.position;
        particleExplosion.Play();
    } 
}
