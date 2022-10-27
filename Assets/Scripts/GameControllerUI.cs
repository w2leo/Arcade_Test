using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameControllerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemsText;
    [SerializeField] TextMeshProUGUI timerText;

    private GameController gameController;

    private void Start()
    {
        gameController = GetComponent<GameController>();
        GameController.NotifyGameState += ChangeTextState;
    }

    private void Update()
    {
        timerText.text = $"{(int)gameController.RemainTime} sec. left";
        itemsText.text = $"Items: {gameController.CollectedItems} / {gameController.SpawnedItems}";
    }

    private void ChangeTextState(GameState gameState)
    {
        bool newState = gameState == GameState.Active;
        itemsText.gameObject.SetActive(newState);
        timerText.gameObject.SetActive(newState);
    }
}
