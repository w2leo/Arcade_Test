using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeveloperTools : MonoBehaviour
{
    [SerializeField] TMP_UserInput inputScreensForItem;
    [SerializeField] private TMP_UserInput inputLevelTime;
    [SerializeField] private TMP_UserInput inputXscale;
    [SerializeField] private TMP_UserInput inputZscale;
    [SerializeField] private Transform errorMessage;

    public Vector2Int GetGroundScale()
    {
        return new Vector2Int(inputXscale.InputValue, inputZscale.InputValue);
    }

    public int GetScreensForItem()
    {
        return inputScreensForItem.InputValue;
    }

    public int GetTimeForItem()
    {
        return inputLevelTime.InputValue;
    }

    public void ShowErrorMessage(string errorMessage)
    {
        StartCoroutine(ShowErrorMessageCoroutine(errorMessage));
    }

    IEnumerator ShowErrorMessageCoroutine(string messageText)
    {
        TextMeshProUGUI errorText = errorMessage.GetComponentInChildren<TextMeshProUGUI>();
        errorText.text = messageText;
        errorMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        errorMessage.gameObject.SetActive(false);
    }
}
