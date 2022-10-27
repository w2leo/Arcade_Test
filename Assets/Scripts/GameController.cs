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
    [SerializeField] TMP_UserInput inputScreensForItem;
    [SerializeField] TMP_UserInput inputLevelTime;
    [SerializeField] TMP_UserInput inputXscale;
    [SerializeField] TMP_UserInput inputZscale;
    [SerializeField] MenuController menuPanel;
    [SerializeField] CameraMove mainCameraMove;
    [SerializeField] Ground ground;

    private Dictionary<int, Item> itemKeyList = new Dictionary<int, Item>();
    private float maxLevelTime;
    private float currentTime;
    private int spawnedItems;
    
    //public static bool GameIsActive { get; private set; }
    public static GameState CurrentGameState { get; private set; }


    public int RemainItems => itemKeyList.Count;

    public int CollectedItems => spawnedItems - RemainItems;

    private void Awake()
    {
        //ChangeTextState(false);
        CurrentGameState = GameState.NotStarted;
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
        ground.ChangeSize(inputXscale.InputValue, inputZscale.InputValue);
        spawnedItems = CountItemsToSpawn();

        if (spawnedItems < 1)
        {
            throw new System.Exception("Wrong scene initialization. 0 items to spawn");
        }

        maxLevelTime = inputLevelTime.InputValue;
        currentTime = 0;
        ChangeTextState(true);
        ItemListInit();
        SpawnPlayer();

        for (int i = 0; i < spawnedItems; i++)
        {
            itemKeyList.Add(i, spawner.SpawnNewItem(ground.xMax, ground.zMax, this, i));
        }
        CurrentGameState = GameState.Active;

    }

    public void CollectItem(Item item)
    {
        int index = item.ItemIndex;
        itemKeyList.Remove(item.ItemIndex);
        Destroy(item.gameObject);
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Active)
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
        ChangeTextState(false);

        // Show EndGame Panel
        if (RemainItems > 0)
        {
            CurrentGameState = GameState.Loose;
            // Loose Panel
            Debug.Log("You loose");
        }
        else
        {
            CurrentGameState = GameState.Win;
            // Win Panel
            Debug.Log("You win");
        }
        menuPanel.ShowEndGamePanen(CurrentGameState);
    }



    private void ChangeTextState(bool state)
    {
        itemsText.gameObject.SetActive(state);
        timerText.gameObject.SetActive(state);
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
        int result = Mathf.RoundToInt(screensCount / inputScreensForItem.InputValue);
        return result;
    }
}
