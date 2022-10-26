using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class TMP_InputInt : MonoBehaviour
{
    private int inputValue;
    private TMP_InputField field;
    private int defaultIntValue = 1;

    public int InputValue => inputValue;

    private void Awake()
    {
        field = GetComponent<TMP_InputField>();
    }
    
    public void CheckIntInput()
    {
        Debug.Log("Check input INT");
        if (!int.TryParse(field.text, out inputValue))
        {
            inputValue = defaultIntValue;
            field.text = inputValue.ToString();
        }
    }
}
