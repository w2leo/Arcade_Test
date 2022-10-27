using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    [SerializeField] Player player;
    [SerializeField] MenuController menuPanel;
    [SerializeField] CameraMove mainCameraMove;
    [SerializeField] Ground ground;
    [SerializeField] DeveloperTools devTools; // instead of Game Conditions

    private Dictionary<int, Item> itemKeyList = new Dictionary<int, Item>();
    private float maxLevelTime;
    private float currentLevelTime;
    private int spawnedItems;

    public static GameState CurrentGameState { get; private set; }
    public int RemainItems => itemKeyList.Count;
    public int SpawnedItems => spawnedItems;
    public int CollectedItems => spawnedItems - RemainItems;
    public float RemainTime => Mathf.Round(maxLevelTime - currentLevelTime);

    public delegate void ControllerHandler(GameState gameState);
    public static event ControllerHandler NotifyGameState;

    private void Start()
    {
        ChangeGameState(GameState.Stop);
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Active)
        {
            currentLevelTime += Time.deltaTime;
            if (currentLevelTime > maxLevelTime || RemainItems == 0)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(LevelInitialization());
    }

    public void EndGame()
    {
        ChangeGameState(RemainItems == 0 ? GameState.Win : GameState.Loose);
    }

    public void CollectItem(Item item)
    {
        int index = item.ItemIndex;
        itemKeyList.Remove(item.ItemIndex);
        Destroy(item.gameObject);
    }

    private void ChangeGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;
        NotifyGameState(newGameState);
    }

    private void ItemListInit()
    {
        foreach (var keyPair in itemKeyList)
        {
            Destroy(keyPair.Value.gameObject);
        }
        itemKeyList.Clear();
    }

    private void AddItemsToDictionary()
    {
        for (int i = 0; i < spawnedItems; i++)
        {
            itemKeyList.Add(i, spawner.SpawnNewItem(ground.xMax, ground.zMax, this, i));
        }
    }

    private void SetLevelTime()
    {
        maxLevelTime = spawnedItems * devTools.GetTimeForItem();
        currentLevelTime = 0;
    }

    private void SpawnPlayer()
    {
        player.transform.position = Position.GetRandomPosition(ground.xMax, ground.zMax);
        player.gameObject.SetActive(true);
    }

    private int CountItemsToSpawn()
    {
        mainCameraMove.SetCameraToDefaultPosition();
        int screensCount = Mathf.RoundToInt((ground.Area / CameraViewState.ViewArea));
        int result = Mathf.RoundToInt(screensCount / devTools.GetScreensForItem());
        return result;
    }

    IEnumerator LevelInitialization()
    {
        ground.ChangeSize(devTools.GetGroundScale());
        yield return null;      // Go to nex Frame after Ground changed size
        spawnedItems = CountItemsToSpawn();

        if (spawnedItems < 1)
        {
            devTools.ShowErrorMessage("Wrong scene initialization.  items to spawn");
            yield break;
        }
        SetLevelTime();
        ItemListInit();
        SpawnPlayer();
        AddItemsToDictionary();
        ChangeGameState(GameState.Active);
    }
}
