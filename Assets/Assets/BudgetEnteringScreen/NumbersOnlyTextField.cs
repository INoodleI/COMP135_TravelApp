using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

// needed for Regex

[RequireComponent(typeof(TMP_InputField))]
public class NumbersOnlyTextField : MonoBehaviour
{
    string text;
    public TMP_InputField textField;
    public bool dollarSign;
    public static bool isChanging;

    private void Start()
    {
        textField = GetComponent<TMP_InputField>();
        isChanging = false;
    }

    public void grabNumbersOnly()
    {
        if (isChanging)
            return;
        isChanging = true;
        text = textField.text;
        String beforeCut = text.Substring(0, textField.caretPosition);
        bool needsCaretReposition = (text.Length == textField.caretPosition);
        
        text = Regex.Replace(text, @"[^0-9]", "");
        beforeCut = Regex.Replace(beforeCut, @"[^0-9]", "");

        if(needsCaretReposition)
            textField.caretPosition = beforeCut.Length-1;
        
        if (dollarSign)
        {
            text = "$" + text;
            textField.text = text;
            if(textField.caretPosition==0)
                textField.caretPosition++;
            isChanging = false;
            return;
        }
        textField.text = text;
        isChanging = false;
    }
}
