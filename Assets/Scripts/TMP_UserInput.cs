using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class TMP_UserInput : MonoBehaviour
{
    private int inputValue;
    private TMP_InputField field;
    private readonly int defaultIntValue = 1;

    public int InputValue => inputValue;

    private void Awake()
    {
        field = GetComponent<TMP_InputField>();
        CheckIntInput();
    }
    
    public void CheckIntInput()
    {
        if (!TryParceIntNotZero(field.text, out inputValue))
        {
            inputValue = defaultIntValue;
            field.text = inputValue.ToString();
        }
    }

    private bool TryParceIntNotZero(string text, out int intValue)
    {
        if (!int.TryParse(text, out intValue) || intValue == 0)
        {
            return false;
        }
        //else if (intValue == 0)
        //{
        //    return false;
        //}
        return true;
    }
}
