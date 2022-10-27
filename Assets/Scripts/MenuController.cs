using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private const string WIN_TEXT = "You WIN!";
    private const string LOOSE_TEXT = "You Loose";

    [SerializeField] TextMeshProUGUI finalText;

    private void Start()
    {
        GameController.NotifyGameState += MenuHandle;
    }

    private void ShowEndGameMenu(string text)
    {
        gameObject.SetActive(true);
        finalText.gameObject.SetActive(true);
        finalText.text = text;
    }

    private void ShowStartMenu()
    {
        gameObject.SetActive(true);
        finalText.gameObject.SetActive(false);
    }

    private void HideMenu()
    {
        gameObject.SetActive(false);
    }


    private void MenuHandle(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Win:
                {
                    ShowEndGameMenu(WIN_TEXT);
                    break;
                }
            case GameState.Loose:
                {
                    ShowEndGameMenu(LOOSE_TEXT);
                    break;
                }
            case GameState.Stop:
                {
                    ShowStartMenu();
                    break;
                }
            case GameState.Active:
                {
                    HideMenu();
                    break;
                }
        }
    }
}
