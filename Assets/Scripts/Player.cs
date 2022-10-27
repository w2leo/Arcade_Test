using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody playerRb;

    private void Start()
    {
        gameObject.SetActive(false);
        playerRb = GetComponent<Rigidbody>();
        GameController.NotifyGameState += PlayerHandle;
    }

    public void PlayerHandle(GameState gameState)
    {
        playerRb.isKinematic = gameState != GameState.Active;
        gameObject.SetActive(gameState == GameState.Active);
    }
}
