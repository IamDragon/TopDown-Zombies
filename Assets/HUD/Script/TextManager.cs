using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    protected TextMeshProUGUI textElement;

    protected virtual void Start()
    {
        textElement = GetComponent<TextMeshProUGUI>();
    }

    protected virtual void UpdateText()
    {

    }
}
