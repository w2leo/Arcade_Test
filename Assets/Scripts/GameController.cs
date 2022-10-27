using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI itemsText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TMP_InputInt inputScreensForItem;
    [SerializeField] TMP_InputInt inputLevelTime;
    [SerializeField] CameraMove mainCameraMove;
    [SerializeField] Ground ground;

    private Dictionary<int, Item> itemKeyList = new Dictionary<int, Item>();
    private float maxLevelTime;
    private float currentTime;
    private int spawnedItems;
    
    public static bool GameIsActive { get; private set; }

    public int RemainItems => itemKeyList.Count;

    public int CollectedItems => spawnedItems - RemainItems;

    private void Awake()
    {
        ChangeTextState(false);
        GameIsActive = false;
    }

    private void ItemListInit()
    {
        foreach (var keyPair in itemKeyList)
        {
            Destroy(keyPair.Value.gameObject);
        }
        itemKeyList.Clear();
    }

    public void StartGame()
    {
        GameIsActive = true;
        maxLevelTime = inputLevelTime.InputValue;
        currentTime = 0;
        ChangeTextState(true);
        ItemListInit();
        SpawnPlayer();

        spawnedItems = CountItemsToSpawn();

        for (int i = 0; i < spawnedItems; i++)
        {
            itemKeyList.Add(i, spawner.SpawnNewItem(ground.xMax, ground.zMax, this, i));
        }
    }

    public void CollectItem(Item item)
    {
        int index = item.ItemIndex;
        itemKeyList.Remove(item.ItemIndex);
        Destroy(item.gameObject);
    }

    private void Update()
    {
        if (GameIsActive)
        {
            currentTime += Time.deltaTime;
            timerText.text = $"{(int)(maxLevelTime - currentTime)} sec. left";
            itemsText.text = $"Items: {CollectedItems} / {spawnedItems}";

            if (currentTime > maxLevelTime || RemainItems == 0)
            {
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        GameIsActive = false;
        ChangeTextState(false);
        // Show EndGame Panel
        if (RemainItems > 0)
        {
            // Loose Panel
            Debug.Log("You loose");
        }
        else
        {
            // Win Panel
            Debug.Log("You win");
        }

    }



    private void ChangeTextState(bool state)
    {
        itemsText.enabled = state;
        timerText.enabled = state;
    }

    private void SpawnPlayer()
    {
        player.transform.position = Position.GetRandomPosition(ground.xMax, ground.zMax);
    }

    private int CountItemsToSpawn()
    {
        mainCameraMove.SetCameraToDefaultPosition();
        int screensCount = Mathf.RoundToInt((ground.Area / CameraViewState.ViewArea));
        int result = Mathf.RoundToInt(screensCount / inputScreensForItem.InputValue);
        return result;
    }
}
