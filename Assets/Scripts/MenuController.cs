using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private const string WIN_TEXT = "You WIN!";
    private const string LOOSE_TEXT = "You Loose";

    [SerializeField] TextMeshProUGUI finalText;

    public void ShowEndGamePanen(GameState result)
    {
        gameObject.SetActive(true);
        if (result == GameState.Win)
        {
            finalText.text = WIN_TEXT;
        }
        else if (result == GameState.Loose)
        {
            finalText.text = LOOSE_TEXT;
        }
    }

    // subscribe to GameController events;
}
