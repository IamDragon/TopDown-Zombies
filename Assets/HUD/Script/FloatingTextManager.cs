using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public FloatingText textPrefab;
    public Transform textPosition;
    [SerializeField] private Vector2 textDirection;
    [SerializeField] private Vector2 dirVariance;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
            txt.UpdateFloatingText();
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        //Transfer world space to screen space so we can use it in the UI
        floatingText.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.direction = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    public void Show(string msg, Color color)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.txt.text = msg;
        floatingText.txt.color = color;
        //Transfer world space to screen space so we can use it in the UI
        //floatingText.transform.position = Camera.main.WorldToScreenPoint(textPosition.position);
        floatingText.transform.position = textPosition.position;
        floatingText.direction = GetRandomisedDirection();

        floatingText.Show();
    }

    private Vector2 GetRandomisedDirection()
    {
        Vector2 dir = textDirection.normalized;
        dir.x += Random.Range(-dirVariance.x, dirVariance.x);
        dir.y += Random.Range(-dirVariance.y, dirVariance.y);
        return dir.normalized;
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            //txt = new FloatingText();
            txt = Instantiate(textPrefab);
            txt.transform.SetParent(textContainer.transform);
            txt.txt = txt.GetComponent<TextMeshProUGUI>();
            floatingTexts.Add(txt);
        }

        return txt;
    }
}
