using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleTake;

    
    private void OnTriggerEnter(Collider other)
    {
        CompareSpeedTest.TEST_COMPARE<Player, Item>(other.transform, "Player", "MainCamera", 100000);
        
        //if (other.TryGetComponent<Player>(out Player player))
        //{
        //    particleTake.transform.position = transform.position;
        //    particleTake.Play();
        //    gameObject.SetActive(false);
        //}

    }

 
}
