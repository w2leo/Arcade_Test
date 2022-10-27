using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Item> itemPrefabs;
    [SerializeField] ParticleSystem particleExplosion;

    public Item SpawnNewItem(float xMax, float zMax, GameController gameController, int index)
    {
        Item newItem = Instantiate(ChooseItemToSpawn(itemPrefabs), transform).GetComponent<Item>();
        newItem.FirstInitialize(Position.GetRandomPosition(xMax, zMax), particleExplosion, gameController, index);
        return newItem;
    }

    private T ChooseItemToSpawn<T>(List<T> items)
    {
        int randIndex = Random.Range(0, items.Count);
        return items[randIndex];
    }
}


