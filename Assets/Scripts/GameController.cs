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
    [SerializeField] TextMeshProUGUI itemsText;
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] MenuController menuPanel;
    [SerializeField] CameraMove mainCameraMove;
    [SerializeField] Ground ground;
    [SerializeField] DeveloperTools devTools;

    private Dictionary<int, Item> itemKeyList = new Dictionary<int, Item>();
    private float maxLevelTime;
    private float currentTime;
    private int spawnedItems;

    public static GameState CurrentGameState { get; private set; }

    public int RemainItems => itemKeyList.Count;

    public int CollectedItems => spawnedItems - RemainItems;

    public delegate void ControllerHandler(GameState gameState);
    public static event ControllerHandler NotifyGameState;

    private void Start()
    {
       ChangeGameState(GameState.Stop);
    }

    private void ItemListInit()
    {
        foreach (var keyPair in itemKeyList)
        {
            Destroy(keyPair.Value.gameObject);
        }
        itemKeyList.Clear();
    }



    IEnumerator LevelInitialization()
    {
        ground.ChangeSize(devTools.GetGroundScale());
        yield return null; // Go to nex Frame after Ground change size
        spawnedItems = CountItemsToSpawn();

        if (spawnedItems < 1)
        {
           // menuPanel.gameObject.SetActive(true);
            devTools.ShowErrorMessage("Wrong scene initialization.  items to spawn");
            yield break;
        }

        maxLevelTime = spawnedItems * devTools.GetTimeForItem();
        currentTime = 0;
        ChangeTextState(true);
        ItemListInit();
        SpawnPlayer();

        for (int i = 0; i < spawnedItems; i++)
        {
            itemKeyList.Add(i, spawner.SpawnNewItem(ground.xMax, ground.zMax, this, i));
        }
        ChangeGameState(GameState.Active);
    }

    private void ChangeGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;
        NotifyGameState(newGameState);
    }

    public void StartGame()
    {
        StartCoroutine(LevelInitialization());
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
            ChangeGameState(GameState.Loose);
            Debug.Log("You loose");
        }
        else
        {
            ChangeGameState(GameState.Win);
            Debug.Log("You win");
        }
        //menuPanel.ShowEndGameMenu(CurrentGameState);
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
        int result = Mathf.RoundToInt(screensCount / devTools.GetScreensForItem());
        return result;
    }
}
