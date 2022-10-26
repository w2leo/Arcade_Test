using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Item> itemPrefabs;
    [SerializeField] ParticleSystem particleExplosion;
    [SerializeField] Ground ground;
    private List<Item> spawnedItems;

    private void Awake()
    {
        spawnedItems = new List<Item>();
        //SpawnItems(CountItemsToSpawn());   
    }

    private int CountItemsToSpawn() // Not Implemented
    {
        return Random.Range(20, 30);
    }

    private void SpawnItems(int countItems)
    {
        for (int i = 0; i < countItems; i++)
        {
            Item newItem = Instantiate(ChooseItemToSpawn(itemPrefabs)).GetComponent<Item>();
            spawnedItems.Add(newItem);
            newItem.FirstInitialize(GetRandomPosition(ground.xMax, ground.zMax, newItem.Size), particleExplosion);
        }
    }

    private T ChooseItemToSpawn<T>(List<T> items)
    {
        int randIndex = Random.Range(0, items.Count);
        return items[randIndex];
    }

    private Vector3 GetRandomPosition(float xMax, float zMax, float itemSize, float yMax = 0f)
    {
        float xRand = Random.Range(-xMax, xMax);
        float yRand = Random.Range(-yMax, yMax);
        float zRand = Random.Range(-zMax, zMax);
        return new Vector3(xRand, yRand, zRand);
    }
}
