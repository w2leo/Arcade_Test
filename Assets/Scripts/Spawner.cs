using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Item> itemPrefabs;
    [SerializeField] ParticleSystem particleExplosion;
    [SerializeField] Ground ground;
    [SerializeField] CameraMove mainCameraMove;
    [SerializeField] TMP_InputInt inputScreensForItem;
    private List<Item> spawnedItems;
    private int spawnCount;

    private void Awake()
    {
        spawnedItems = new List<Item>();
        mainCameraMove.SetCameraToPlayer();
        SpawnNewItems(CountItemsToSpawn());   
    }

    public void RespawnAllItems()
    {
        DestroyAllItems();
        SpawnNewItems(CountItemsToSpawn());
    }


    private void SpawnNewItems(int countItems)
    {
        for (int i = 0; i < countItems; i++)
        {
            Item newItem = Instantiate(ChooseItemToSpawn(itemPrefabs)).GetComponent<Item>();
            spawnedItems.Add(newItem);
            newItem.FirstInitialize(GetRandomPosition(ground.xMax, ground.zMax, newItem.Size), particleExplosion);
        }
        spawnCount = countItems;
    }

    private int CountItemsToSpawn()
    {
        int screensCount = Mathf.RoundToInt((ground.Area / CameraViewState.ViewArea));
        int result = Mathf.RoundToInt(screensCount / inputScreensForItem.InputValue);
        return result;
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
