using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Item> itemPrefabs;
    [SerializeField] ParticleSystem particleExplosion;
    [SerializeField] Ground ground;
    [SerializeField] CameraMove mainCameraMove;
    private List<Item> spawnedItems;

    private void Awake()
    {
        spawnedItems = new List<Item>();
        mainCameraMove.SetCameraToPlayer();
        SpawnNewItems(CountItemsToSpawn());   
    }

    public void SpawnNewItems()
    {
        DestroyAllItems();
        SpawnNewItems(CountItemsToSpawn());
    }

    private int CountItemsToSpawn() // Not Implemented
    {
        int result = Mathf.RoundToInt((ground.Area / CameraViewState.ViewArea));
        Debug.Log($"rArea = {ground.Area}, viewArea = {CameraViewState.ViewArea}, itemsCount = {result}");
                
        return result;
    }

    private void SpawnNewItems(int countItems)
    {
        for (int i = 0; i < countItems; i++)
        {
            Item newItem = Instantiate(ChooseItemToSpawn(itemPrefabs)).GetComponent<Item>();
            spawnedItems.Add(newItem);
            newItem.FirstInitialize(GetRandomPosition(ground.xMax, ground.zMax, newItem.Size), particleExplosion);
        }
    }

    private void DestroyAllItems()
    {
        foreach (var e in spawnedItems)
        {
            Destroy(e.gameObject);
        }
        spawnedItems.Clear();
    }

    private void DestroyItem(int index)
    {
        Destroy(spawnedItems[index].gameObject);
        spawnedItems.RemoveAt(index);
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
