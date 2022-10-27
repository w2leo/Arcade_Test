using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperTools : MonoBehaviour
{
    [SerializeField] TMP_UserInput inputScreensForItem;
    [SerializeField] private TMP_UserInput inputLevelTime;
    [SerializeField] private TMP_UserInput inputXscale;
    [SerializeField] private TMP_UserInput inputZscale;

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
}
